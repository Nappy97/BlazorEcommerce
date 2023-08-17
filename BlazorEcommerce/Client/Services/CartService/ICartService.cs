namespace BlazorEcommerce.Client.Services.CartService;

public interface ICartService
{
    event Action OnChange;

    // 카트에 담기
    Task AddToCart(CartItem cartItem);

    // 카트에 담긴것 정보얻기(로컬 스토리지)
    // Task<List<CartItem>> GetCartItems();

    // 카트에 담긴것 정보얻기
    Task<List<CartProductResponseDto>> GetCartProducts();

    // 카트에서 상품제거
    Task RemoveProductFromCart(int productId, int productTypeId);

    // 같은 상품일 경우 개수로 변경
    Task UpdateQuantity(CartProductResponseDto product);

    // 로컬스토리지 옮기기
    Task StoreCartItems(bool emptyLocalCart);
    
    // 카트에 담긴것 개수
    Task GetCartItemsCount();
}