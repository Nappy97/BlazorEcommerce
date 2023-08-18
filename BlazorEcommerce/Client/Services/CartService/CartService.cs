using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;
    private readonly IAuthService _authService;

    public CartService(ILocalStorageService localStorage, HttpClient http,
        IAuthService authService)
    {
        _localStorage = localStorage;
        _http = http;
        _authService = authService;
    }

    public event Action OnChange;

    // 카트에 담기
    public async Task AddToCart(CartItem cartItem)
    {
        if (await _authService.IsUserAuthenticated())
        {
            await _http.PostAsJsonAsync("api/cart/add", cartItem);
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId
                                          && x.ProductTypeId == cartItem.ProductTypeId);
            if (sameItem == null)
            {
                cart.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _localStorage.SetItemAsync("cart", cart);
        }

        await GetCartItemsCount();
    }

    // 카트에 담긴것 정보얻기(로컬 스토리지)
    // public async Task<List<CartItem>> GetCartItems()
    // {
    //     await GetCartItemsCount();
    //     var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
    //     if (cart == null)
    //     {
    //         cart = new List<CartItem>();
    //     }
    //
    //     return cart;
    // }

    // 카트에 담긴것 정보얻기
    public async Task<List<CartProductResponseDto>> GetCartProducts()
    {
        if (await _authService.IsUserAuthenticated())
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<CartProductResponseDto>>>("api/cart");
            return response.Data;
        }
        else
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cartItems == null)
                return new List<CartProductResponseDto>();
            var response = await _http.PostAsJsonAsync("api/cart/products", cartItems);
            var cartProducts =
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponseDto>>>();
            return cartProducts.Data;
        }
    }

    // 카트에서 상품제거
    public async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        if (await _authService.IsUserAuthenticated())
        {
            await _http.DeleteAsync($"api/cart/{productId}/{productTypeId}");
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.ProductId == productId
                                          && x.ProductTypeId == productTypeId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }

    // 같은 상품일 경우 개수로 변경
    public async Task UpdateQuantity(CartProductResponseDto product)
    {
        if (await _authService.IsUserAuthenticated())
        {
            var request = new CartItem
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                ProductTypeId = product.ProductTypeId
            };
            await _http.PutAsJsonAsync("api/cart/update-quantity", request);
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.ProductId == product.ProductId
                                          && x.ProductTypeId == product.ProductTypeId);

            if (cartItem != null)
            {
                cartItem.Quantity = product.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }

    // 로컬스토리지 옮기기
    public async Task StoreCartItems(bool emptyLocalCart = false)
    {
        var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (localCart == null)
        {
            return;
        }

        await _http.PostAsJsonAsync("api/cart", localCart);

        if (emptyLocalCart)
        {
            await _localStorage.RemoveItemAsync("cart");
        }
    }

    // 카트에 담긴것 개수
    public async Task GetCartItemsCount()
    {
        if (await _authService.IsUserAuthenticated())
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/Cart/count");
            var count = result.Data;

            await _localStorage.SetItemAsync("cartItemsCount", count);
        }
        else
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            await _localStorage.SetItemAsync("cartItemsCount", cart?.Count ?? 0);
        }

        OnChange.Invoke();
    }
}