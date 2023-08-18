namespace BlazorEcommerce.Shared.Dto;

public class OrderDetailsResponseDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderDetailsProductResponseDto> Products { get; set; }
}