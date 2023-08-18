namespace BlazorEcommerce.Client.Services.OrderService;

public interface IOrderService
{
    // 카트에서 주문하기
    Task<string> PlaceOrder();
    
    // 주문 목록 가져오기
    Task<List<OrderOverviewResponseDto>> GetOrders();
    
    // 주문 상세 정보 가져오기
    Task<OrderDetailsResponseDto> GetOrderDetails(int orderId);
}