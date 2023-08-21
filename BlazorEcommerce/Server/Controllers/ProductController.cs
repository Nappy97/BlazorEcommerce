using BlazorEcommerce.Shared.Dto;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;
using BlazorEcommerce.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
    {
        var result = await _productService.GetProductsAsync();
        return Ok(result);
    }

    // [HttpGet]
    // [Route("{productId:int}")]
    [HttpGet("{productId:int}")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
    {
        var result = await _productService.GetProductAsync(productId);
        return Ok(result);
    }

    [HttpGet("category/{categoryUrl}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string categoryUrl)
    {
        var result = await _productService.GetProductByCategory(categoryUrl);
        return Ok(result);
    }

    [HttpGet("search/{searchText}/{page:int}")]
    public async Task<ActionResult<ServiceResponse<ProductsSearchResultDto>>> SearchProducts(string searchText,
        int page)
    {
        var result = await _productService.SearchProducts(searchText, page);
        return Ok(result);
    }

    [HttpGet("searchsuggestions/{searchText}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductSearchSuggestions(string searchText)
    {
        var result = await _productService.GetProductSearchSuggestions(searchText);
        return Ok(result);
    }

    [HttpGet("featured")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
    {
        var result = await _productService.GetFeaturedProducts();
        return Ok(result);
    }

    [HttpGet("admin"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Product>>> GetAdminProducts()
    {
        var result = await _productService.GetAdminProducts();
        return Ok(result);
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        var result = await _productService.CreateProduct(product);
        return Ok(result);
    }

    [HttpPut, Authorize(Roles = "Admin")]
    public async Task<ActionResult<Product>> UpdateProduct(Product product)
    {
        var result = await _productService.UpdateProduct(product);
        return Ok(result);
    }

    [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProduct(id);
        return Ok(result);
    }
}