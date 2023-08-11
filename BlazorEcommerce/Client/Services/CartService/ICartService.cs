using BlazorEcommerce.Shared.Dto;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Internal;

namespace BlazorEcommerce.Client.Services.CartService;

public interface ICartService
{
    event Action OnChange;

    Task AddToCart(CartItem cartItem);

    Task<List<CartItem>> GetCartItems();

    Task<List<CartProductResponseDto>> GetCartProducts();

    Task RemoveProductFromCart(int productId, int productTypeId);

    Task UpdateQuantity(CartProductResponseDto product);

    Task StoreCartItems(bool emptyLocalCart);
}