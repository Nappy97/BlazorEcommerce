using BlazorEcommerce.Shared.Dto;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Internal;
using BlazorEcommerce.Shared.Response;

namespace BlazorEcommerce.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly DataContext _context;

    public CartService(DataContext context)
    {
        _context = context;
    }

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
    public async Task<ServiceResponse<List<CartProductResponseDto>>> StoreCartItems(List<CartItem> cartItems,
        int userId)
    {
        cartItems.ForEach(cartItem => cartItem.UserId = userId);
        _context.CartItems.AddRange(cartItems);
        await _context.SaveChangesAsync();

        return await GetCartProducts(
            await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToListAsync());
    }
}