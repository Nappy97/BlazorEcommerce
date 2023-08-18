using System.Security.Claims;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Shared;

public partial class AdminMenu
{
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; }

    private bool _authorized = false;

    protected override async Task OnInitializedAsync()
    {
        string role = (await AuthStateProvider.GetAuthenticationStateAsync())
            .User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

        if (role.Contains("Admin"))
        {
            _authorized = true;
        }
    }
}