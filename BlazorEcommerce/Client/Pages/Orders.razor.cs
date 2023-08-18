using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class Orders
{
    [Inject] IOrderService OrderService { get; set; }

    private List<OrderOverviewResponseDto> _orders = null;

    protected override async Task OnInitializedAsync()
    {
        _orders = await OrderService.getOrders();
    }
}