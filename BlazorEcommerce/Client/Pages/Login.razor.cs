using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class Login
{
    [Inject] private IAuthService AuthService { get; set; }
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private UserLogin _user = new();
    
    private string _errorMessage = string.Empty;

    private async Task HandleLogin()
    {
        var result = await AuthService.Login(_user);
        if (result.Success)
        {
            _errorMessage = string.Empty;
            await LocalStorage.SetItemAsync("authToken", result.Data);
            Console.WriteLine(await LocalStorage.GetItemAsync<string>("authToken"));
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo("");
        }
        else
        {
            _errorMessage = result.Message;
        }
    }
}