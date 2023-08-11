namespace BlazorEcommerce.Server.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);

    // 이메일 중복검사
    Task<bool> UserExists(string email);
    
    // 로그인
    Task<ServiceResponse<string>> Login(string email, string password);

    // 비밀번호 변경
    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
}