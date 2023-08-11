using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared.Model.Internal;

public class UserRegister
{
    [Required(ErrorMessage = "필수입니다."), EmailAddress(ErrorMessage = "이메일 형식으로 입력해주세요!")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "필수입니다."), StringLength(100, MinimumLength = 6, ErrorMessage = "최소 6글자 이상 입력해주세요")]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "마 다르다!")]
    public string ConfirmPassword { get; set; } = string.Empty;
}