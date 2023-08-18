namespace BlazorEcommerce.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthService(HttpClient http, AuthenticationStateProvider authenticationStateProvider)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
    }
    
    // 회원가입
    public async Task<ServiceResponse<int>> Register(UserRegister request)
    {
        var result = await _http.PostAsJsonAsync("api/auth/register", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    // 로그인
    public async Task<ServiceResponse<string>> Login(UserLogin request)
    {
        var result = await _http.PostAsJsonAsync("api/auth/login", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    // 비밀번호 변경
    public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
    {
        var result = await _http.PostAsJsonAsync("api/auth/change-password", request.Password);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    // 유저가 인증되었는지 확인하는 메서드
    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authenticationStateProvider.GetAuthenticationStateAsync())
            .User.Identity.IsAuthenticated;
    }
}