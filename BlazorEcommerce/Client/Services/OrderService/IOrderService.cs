namespace BlazorEcommerce.Client.Services.OrderService;

public interface IOrderService
{
    // 카트에서 주문하기
    Task PlaceOrder();
    
    // 주문 목록 가져오기
    Task<List<OrderOverviewResponseDto>> getOrders();
}