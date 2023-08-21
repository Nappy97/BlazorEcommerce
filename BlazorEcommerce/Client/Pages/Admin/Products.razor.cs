using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages.Admin;

[Authorize(Roles = "Admin")]
public partial class Products
{
    [Inject] private IProductService ProductService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ProductService.GetAdminProducts();
    }

    void EditProduct(int productId)
    {
        NavigationManager.NavigateTo($"/admin/product/{productId}");
    }

    void CreateProduct()
    {
        NavigationManager.NavigateTo("/admin/product");
    }
}