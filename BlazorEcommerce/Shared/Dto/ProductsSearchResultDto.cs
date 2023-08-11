using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;

namespace BlazorEcommerce.Shared.Dto;

public class ProductsSearchResultDto
{
    public List<Product> Products { get; set; } = new();
    
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
}