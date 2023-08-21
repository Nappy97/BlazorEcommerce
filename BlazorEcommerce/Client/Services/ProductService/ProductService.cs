using BlazorEcommerce.Shared.Dto;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;
using BlazorEcommerce.Shared.Response;

namespace BlazorEcommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public event Action ProductChanged;
    public List<Product> Products { get; set; } = new();
    public List<Product> AdminProducts { get; set; } /* = new();*/
    public string Message { get; set; } = "Loading products...";
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;
    public string LastSearchText { get; set; } = string.Empty;

    public async Task GetProducts(string? categoryUrl = null)
    {
        var result = categoryUrl == null
            ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/featured")
            : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/category/{categoryUrl}");
        if (result is { Data: not null })
            Products = result.Data;

        CurrentPage = 1;
        PageCount = 0;
        if (Products.Count == 0)
        {
            Message = "No Products found";
        }

        ProductChanged.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result =
            await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
        return result;
    }

    public async Task<List<string>> GetProductSearchSuggestions(string searchText)
    {
        var result = await _http
            .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Product/searchsuggestions/{searchText}");
        return result.Data;
    }

    public async Task GetAdminProducts()
    {
        var result = await _http
            .GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/admin");

        AdminProducts = result.Data;
        CurrentPage = 1;
        PageCount = 0;
        if (AdminProducts is { Count: 0 })
            Message = "No Products found";
    }

    public async Task<Product> CreateProduct(Product product)
    {
        var result = await _http.PostAsJsonAsync("api/Product", product);
        var newProduct = (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;

        return newProduct;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var result = await _http.PutAsJsonAsync("api/Product", product);
        var updatedProduct = (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        
        return updatedProduct;
    }

    public async Task DeleteProduct(Product product)
    {
        var result = await _http.DeleteAsync($"api/Product/{product.Id}"); 
    }

    public async Task SearchProducts(string searchText, int page)
    {
        LastSearchText = searchText;
        var result = await _http
            .GetFromJsonAsync<ServiceResponse<ProductsSearchResultDto>>($"api/product/search/{searchText}/{page}");
        if (result is { Data: not null })
        {
            Products = result.Data.Products;
            CurrentPage = result.Data.CurrentPage;
            PageCount = result.Data.Pages;
        }

        if (Products.Count == 0)
            Message = "No Products found.";
        ProductChanged?.Invoke();
    }
}