using System.Net.Http.Json;
using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Shared;

public partial class ProductList
{
    [Inject] private IProductService _productService { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await _productService.GetProducts();
    }
}