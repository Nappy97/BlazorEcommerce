namespace BlazorEcommerce.Shared.Model.Data;

public class Address
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Country { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Gu { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string DetailAddress { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
}