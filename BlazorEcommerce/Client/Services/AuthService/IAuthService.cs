namespace BlazorEcommerce.Client.Services.AuthService;

public interface IAuthService
{
    // Register
    Task<ServiceResponse<int>> Register(UserRegister request);

    // Login
    Task<ServiceResponse<string>> Login(UserLogin request);
    
    // 비밀번호 변경
    Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
    
}