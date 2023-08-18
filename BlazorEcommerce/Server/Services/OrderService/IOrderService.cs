namespace BlazorEcommerce.Server.Services.OrderService;

public interface IOrderService
{
    // 장바구니에서 주문을 생성합니다.
    Task<ServiceResponse<bool>> PlaceOrder(int userId);
    
    // 주문목록을 가져옵니다.
    Task<ServiceResponse<List<OrderOverviewResponseDto>>> GetOrders();
    
    // 주문 상세정보를 가져옵니다.
    Task<ServiceResponse<OrderDetailsResponseDto>> GetOrderDetails(int orderId);
}