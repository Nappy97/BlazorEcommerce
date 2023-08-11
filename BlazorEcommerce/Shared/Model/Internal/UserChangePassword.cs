using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared.Model.Internal;

public class UserChangePassword
{
    [Required, StringLength(100, MinimumLength = 6, ErrorMessage = "6자 이상, 100자 이하")]
    public string Password { get; set; } = string.Empty;
    
    [Compare("Password", ErrorMessage = "달라요!")]
    public string ConfirmPassword { get; set; } = string.Empty;
}