using BlazorEcommerce.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorEcommerce.Client.Pages.Admin;

public partial class EditProduct
{
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] IProductService ProductService { get; set; }
    [Inject] IProductTypeService ProductTypeService { get; set; }
    [Inject] ICategoryService CategoryService { get; set; }
    [Inject] IJSRuntime JsRuntime { get; set; }

    [Parameter] public int Id { get; set; }

    private Product _product { get; set; } = new();
    bool _loading = true;
    string _btnText = "";
    string _message = "Loading...";

    protected override async Task OnInitializedAsync()
    {
        await ProductTypeService.GetProductTypes();
        await CategoryService.GetAdminCategories();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Id == 0)
        {
            _product = new Product { IsNew = true };
            _btnText = "Create Product";
        }
        else
        {
            Product dbProduct = (await ProductService.GetProduct(Id)).Data;
            if (dbProduct == null)
            {
                _message = $"Product with id {Id} not found";
                return;
            }

            _product = dbProduct;
            _product.Editing = true;
            _btnText = "Update Product";
        }

        _loading = false;
    }

    void RemoveVariant(int productTypeId)
    {
        var variant = _product.Variants.Find(v => v.ProductTypeId == productTypeId);

        if (variant == null)
        {
            return;
        }

        if (variant.IsNew)
        {
            _product.Variants.Remove(variant);
        }
        else
        {
            variant.Deleted = true;
        }
    }

    void AddVariant()
    {
        _product.Variants.Add(new ProductVariant { IsNew = true, ProductId = _product.Id });
    }

    async void AddOrUpdateProduct()
    {
        if (_product.IsNew)
        {
            var result = await ProductService.CreateProduct(_product);
            NavigationManager.NavigateTo($"admin/product/{result.Id}");
        }
        else
        {
            _product.IsNew = false;
            _product = await ProductService.UpdateProduct(_product);
            NavigationManager.NavigateTo($"admin/product/{_product.Id}", true);
        }
    }

    async void DeleteProduct()
    {
        // 한번 더 확인
        bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", $"진짜로 '{_product.Title}'을 삭제하시겠습니까?");
        
        if (!confirmed) 
            return;
        
        await ProductService.DeleteProduct(_product);
        NavigationManager.NavigateTo("admin/products");
    }
}