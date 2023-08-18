using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly NavigationManager _navigationManager;
    private readonly HttpClient _http;

    public OrderService(HttpClient http, AuthenticationStateProvider authStateProvider,
        NavigationManager navigationManager)
    {
        _http = http;
        _authStateProvider = authStateProvider;
        _navigationManager = navigationManager;
    }

    // 카트에서 주문하기
    public async Task PlaceOrder()
    {
        if (await IsUserAuthenticated())
        {
            await _http.PostAsync("api/order", null);
        }
        else
        {
            _navigationManager.NavigateTo("login");
        }
    }

    // 주문 목록 가져오기
    public async Task<List<OrderOverviewResponseDto>> getOrders()
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponseDto>>>("api/order");
        return result.Data;
    }

    private async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }
}