namespace BlazorEcommerce.Client.Shared;

public partial class UserButton
{
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
}