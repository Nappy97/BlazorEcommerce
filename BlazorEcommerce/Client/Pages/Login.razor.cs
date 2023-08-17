using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorEcommerce.Client.Pages;

public partial class Login
{
    [Inject] private IAuthService AuthService { get; set; }
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private ICartService CartService { get; set; }

    private UserLogin _user = new();

    private string _errorMessage = string.Empty;

    private string _returnUrl = string.Empty;

    protected override void OnInitialized()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
        {
            _returnUrl = url;
        }
    }

    private async Task HandleLogin()
    {
        var result = await AuthService.Login(_user);
        if (result.Success)
        {
            _errorMessage = string.Empty;
            await LocalStorage.SetItemAsync("authToken", result.Data);
            Console.WriteLine(await LocalStorage.GetItemAsync<string>("authToken"));
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            await CartService.StoreCartItems(true);
            await CartService.GetCartItemsCount();
            NavigationManager.NavigateTo(_returnUrl);
        }
        else
        {
            _errorMessage = result.Message;
        }
    }
}