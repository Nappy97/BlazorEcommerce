using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // 장바구니에서 주문하기
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrder()
    {
        var result = await _orderService.PlaceOrder();
        return Ok(result);
    }

    // 주문목록 가져오기
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponseDto>>>> GetOrders()
    {
        var result = await _orderService.GetOrders();
        return Ok(result);
    }
}