using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class Register
{
    [Inject] private IAuthService AuthService { get; set; }
    
    private UserRegister _user = new();

    private string _message = string.Empty;

    private string _messageCssClass = string.Empty;

    async Task HandleRegistration()
    {
        var result = await AuthService.Register(_user);
        _message = result.Message;
        if (result.Success)
            _messageCssClass = "text-success";
        else
            _messageCssClass = "text-danger";
    }
}