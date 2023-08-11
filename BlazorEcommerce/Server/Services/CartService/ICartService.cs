using BlazorEcommerce.Shared.Dto;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Internal;
using BlazorEcommerce.Shared.Response;

namespace BlazorEcommerce.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems);
}