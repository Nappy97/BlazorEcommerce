using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Shared;

public partial class FeaturedProducts : IDisposable
{
    [Inject] private IProductService ProductService { get; set; }

    protected override void OnInitialized()
    {
        ProductService.ProductChanged += StateHasChanged;
    }

    public void Dispose()
    {
        ProductService.ProductChanged -= StateHasChanged;
    }
}