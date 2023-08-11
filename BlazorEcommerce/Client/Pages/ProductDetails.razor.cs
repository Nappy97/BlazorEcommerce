using BlazorEcommerce.Shared.Model.Data;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Internal;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class ProductDetails
{
    [Inject] private IProductService ProductService { get; set; }
    [Inject] private ICartService CartService { get; set; }

    private Product? product = null;
    private string message = string.Empty;
    private int currentTypeId = 1;

    [Parameter] public int Id { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        message = "Loading Product";
        var result = await ProductService.GetProduct(Id);
        if (!result.Success)
        {
            message = result.Message;
        }
        else
        {
            product = result.Data;
            if (product is { Variants.Count: > 0 })
            {
                currentTypeId = product.Variants[0].ProductTypeId;
            }
        }
    }

    private ProductVariant GetSelectVariant()
    {
        var variant = product.Variants.FirstOrDefault(v => v.ProductTypeId == currentTypeId);
        return variant;
    }

    private async Task AddToCart()
    {
        var productVariant = GetSelectVariant();
        var cartItem = new CartItem()
        {
            ProductId = productVariant.ProductId,
            ProductTypeId = productVariant.ProductTypeId
        };

        await CartService.AddToCart(cartItem);
    }
}