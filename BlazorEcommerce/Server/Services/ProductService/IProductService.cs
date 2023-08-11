using BlazorEcommerce.Shared.Dto;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;
using BlazorEcommerce.Shared.Response;

namespace BlazorEcommerce.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductsAsync();

    Task<ServiceResponse<Product>> GetProductAsync(int productId);

    Task<ServiceResponse<List<Product>>> GetProductByCategory(string categoryUrl);

    Task<ServiceResponse<ProductsSearchResultDto>> SearchProducts(string searchText, int page);

    Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
    
    Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
}