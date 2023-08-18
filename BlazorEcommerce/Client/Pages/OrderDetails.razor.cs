using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class OrderDetails
{
    [Inject] IOrderService OrderService { get; set; }
    
    [Parameter] public int OrderId { get; set; }

    private OrderDetailsResponseDto _order = null;
    
    protected override async Task OnInitializedAsync()
    {
        _order = await OrderService.GetOrderDetails(OrderId);
    }
}