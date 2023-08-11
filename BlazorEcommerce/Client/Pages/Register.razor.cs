using BlazorEcommerce.Shared.Model.Internal;

namespace BlazorEcommerce.Client.Pages;

public partial class Register
{
    private UserRegister user = new UserRegister();

    void HandleRegistration()
    {
        Console.WriteLine($"Register User with Email {user.Email}.");
    }
}