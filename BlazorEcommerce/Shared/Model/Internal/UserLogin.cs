using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared.Model.Internal;

public class UserLogin
{
    [Required(ErrorMessage = "아이디는 필수 입력값입니다.")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "비밀번호는 필수 입력값입니다.")]
    public string Password { get; set; } = string.Empty;
}