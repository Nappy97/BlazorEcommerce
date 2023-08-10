﻿namespace BlazorEcommerce.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductsAsync();

    Task<ServiceResponse<Product>> GetProductAsync(int productId);

    Task<ServiceResponse<List<Product>>> GetProductByCategory(string categoryUrl);

    Task<ServiceResponse<ProductsSearchResultDTO>> SearchProducts(string searchText, int page);

    Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
    
    Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
}