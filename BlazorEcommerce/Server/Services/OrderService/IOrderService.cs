namespace BlazorEcommerce.Server.Services.OrderService;

public interface IOrderService
{
    // 장바구니에서 주문을 생성합니다.
    Task<ServiceResponse<bool>> PlaceOrder();
}