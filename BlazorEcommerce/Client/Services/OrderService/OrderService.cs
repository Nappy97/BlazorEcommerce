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
    public async Task<string> PlaceOrder()
    {
        if (await IsUserAuthenticated())
        {
            var result = await _http.PostAsync("api/payment/checkout", null);
            var url = await result.Content.ReadAsStringAsync();
            return url;
        }
        else
        {
            // _navigationManager.NavigateTo("login");
            return "login";
        }
    }

    // 주문 목록 가져오기
    public async Task<List<OrderOverviewResponseDto>> GetOrders()
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponseDto>>>("api/order");
        return result.Data;
    }

    // 주문 상세 정보 가져오기
    public async Task<OrderDetailsResponseDto> GetOrderDetails(int orderId)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponseDto>>($"api/order/{orderId}");
        return result.Data;
    }

    private async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }
}