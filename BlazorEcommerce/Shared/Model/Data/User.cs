namespace BlazorEcommerce.Shared.Model.Data;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public Address Address { get; set; }
    // public int AddressId { get; set; }
}