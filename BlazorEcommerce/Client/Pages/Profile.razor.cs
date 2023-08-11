using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

[Authorize]
public partial class Profile
{
    [Inject] private IAuthService AuthService { get; set; }

    private UserChangePassword request = new();

    private string _message = string.Empty;

    private async Task ChangePassword()
    {
        var result = await AuthService.ChangePassword(request);
        _message = result.Message;
    }
}