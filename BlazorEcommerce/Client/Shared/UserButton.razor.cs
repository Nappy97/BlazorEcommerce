using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Shared;

public partial class UserButton
{
    [Inject] private ILocalStorageService LocalStorage { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private bool _showUserMenu = false;

    private string UserMenuCssClass => _showUserMenu ? "show-menu" : null;

    private void ToggleUserMenu()
    {
        _showUserMenu = !_showUserMenu;
    }

    private async Task HideUserMenu()
    {
        await Task.Delay(200);
        _showUserMenu = false;
    }

    private async Task Logout()
    {
        await LocalStorage.RemoveItemAsync("authToken");
        await AuthenticationStateProvider.GetAuthenticationStateAsync();
        NavigationManager.NavigateTo("");
    }
}