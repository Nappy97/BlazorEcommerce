using System.Net.Http.Json;
using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Shared;

public partial class ProductList : IDisposable
{
    [Inject] private IProductService _productService { get; set; }

    protected override void OnInitialized()
    {
        _productService.ProductChanged += StateHasChanged;
    }

    public void Dispose()
    {
        _productService.ProductChanged -= StateHasChanged;
    }

    private string GetPriceText(Product product)
    {
        var variants = product.Variants;
        switch (variants.Count)
        {
            case 0:
                return string.Empty;
            case 1:
                return $"${variants[0].Price}";
            default:
            {
                var minPrice = variants.Min(v => v.Price);
                return $"Starting at ${minPrice}";
            }
        }
    }
}