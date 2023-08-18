namespace BlazorEcommerce.Server.Services.CartService;

public interface ICartService
{
    // 카트에 담긴것 정보얻기(로컬 스토리지)
    Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems);

    // 카트에 담긴것 정보얻기(회원)
    Task<ServiceResponse<List<CartProductResponseDto>>> StoreCartItems(List<CartItem> cartItems);
    
    // 카트에 담긴것 개수
    Task<ServiceResponse<int>> GetCartItemCount();
    
    // 카트에 담긴것 정보얻기(회원) from DB
    Task<ServiceResponse<List<CartProductResponseDto>>> GetDbCartProducts(); 
    
    // 카트에 추가
    Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
    
    // 수량변경
    Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem);
    
    // 카트에서 상품제거 
    Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId);
}