namespace BlazorEcommerce.Client.Services.AuthService;

public interface IAuthService
{
    // Register
    Task<ServiceResponse<int>> Register(UserRegister request);

    // Login
    Task<ServiceResponse<string>> Login(UserLogin request);
    
    // 비밀번호 변경
    Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
    
    // 유저가 인증되었는지 확인하는 메서드
    Task<bool> IsUserAuthenticated();
}