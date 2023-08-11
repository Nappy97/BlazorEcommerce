namespace BlazorEcommerce.Client.Services.AuthService;

public interface IAuthService
{
    // Register
    Task<ServiceResponse<int>> Register(UserRegister request);

    // Login
    Task<ServiceResponse<string>> Login(UserLogin request);
}