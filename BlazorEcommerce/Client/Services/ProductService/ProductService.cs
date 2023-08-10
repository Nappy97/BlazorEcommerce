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
    public string Message { get; set; } = "Loading products...";

    public async Task GetProducts(string? categoryUrl = null)
    {
        var result = categoryUrl == null
            ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/featured")
            : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/category/{categoryUrl}");
        if (result is { Data: not null })
            Products = result.Data;

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

    public async Task SearchProducts(string searchText)
    {
        var result = await _http
            .GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchText}");
        if (result is { Data: not null })
            Products = result.Data;

        if (Products.Count == 0)
            Message = "No Products found.";
        ProductChanged?.Invoke();
    }
}