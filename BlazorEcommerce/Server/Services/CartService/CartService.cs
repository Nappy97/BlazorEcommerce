using System.Security.Claims;

namespace BlazorEcommerce.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    // 카트에 담긴것 정보얻기(로컬 스토리지)
    public async Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems)
    {
        var result = new ServiceResponse<List<CartProductResponseDto>>
        {
            Data = new List<CartProductResponseDto>()
        };

        foreach (var item in cartItems)
        {
            var product = await _context.Products
                .Where(p => p.Id == item.ProductId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                continue;
            }

            var productVariant = await _context.ProductVariants
                .Where(v => v.ProductId == item.ProductId
                            && v.ProductTypeId == item.ProductTypeId)
                .Include(v => v.ProductType)
                .FirstOrDefaultAsync();

            if (productVariant == null)
            {
                continue;
            }

            var cartProduct = new CartProductResponseDto
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                ProductType = productVariant.ProductType.Name,
                ProductTypeId = productVariant.ProductTypeId,
                Quantity = item.Quantity
            };

            result.Data.Add(cartProduct);
        }

        return result;
    }

    // 카트에 담긴것 정보얻기(회원)
    public async Task<ServiceResponse<List<CartProductResponseDto>>> StoreCartItems(List<CartItem> cartItems)
    {
        cartItems.ForEach(cartItem => cartItem.UserId = GetUserId());
        _context.CartItems.AddRange(cartItems);
        await _context.SaveChangesAsync();

        return await GetDbCartProducts();
    }

    // 카트에 담긴것 개수
    public async Task<ServiceResponse<int>> GetCartItemCount()
    {
        var count = (await _context.CartItems
            .Where(ci => ci.UserId == GetUserId())
            .ToListAsync()).Count;
        return new ServiceResponse<int> { Data = count };
    }

    // 카트에 담긴것 정보얻기(회원) from DB
    public async Task<ServiceResponse<List<CartProductResponseDto>>> GetDbCartProducts()
    {
        return await GetCartProducts(
            await _context.CartItems
                .Where(ci => ci.UserId == GetUserId())
                .ToListAsync());
    }

    // 카트에 추가
    public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
    {
        cartItem.UserId = GetUserId();

        var sameItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId
                                       && ci.ProductTypeId == cartItem.ProductTypeId
                                       && ci.UserId == cartItem.UserId);
        if (sameItem == null)
        {
            _context.CartItems.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true };
    }

    // 수량변경
    public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
    {
        var dbCartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId
                                       && ci.ProductTypeId == cartItem.ProductTypeId
                                       && ci.UserId == GetUserId());

        if (dbCartItem == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Message = "카트에 해당 상품이 없습니다."
            };
        }

        dbCartItem.Quantity = cartItem.Quantity;
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true };
    }

    // 카트에서 상품제거
    public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
    {
        var dbCartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == productId
                                       && ci.ProductTypeId == productTypeId
                                       && ci.UserId == GetUserId());

        if (dbCartItem == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Message = "카트에 해당 상품이 없습니다."
            };
        }

        _context.CartItems.Remove(dbCartItem);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true };
    }
}