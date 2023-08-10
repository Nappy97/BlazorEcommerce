using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class Index
{
    [Inject] private IProductService ProductService { get; set; }

    [Parameter] public string? CategoryUrl { get; set; } = null;
    [Parameter] public string? SearchText { get; set; } = null;

    protected override async Task OnParametersSetAsync()
    {
        if (SearchText != null)
        {
            await ProductService.SearchProducts(SearchText);
        }
        else
        {
            await ProductService.GetProducts(CategoryUrl);
        }
    }
}