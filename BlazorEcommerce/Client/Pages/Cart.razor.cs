using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class Cart
{
    [Inject] private ICartService CartService { get; set; }

    private List<CartProductResponseDto> _cartProducts = null;
    private string _message = "Loading cart...";

    protected override async Task OnInitializedAsync()
    {
        await LoadCart();
    }

    private async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        await CartService.RemoveProductFromCart(productId, productTypeId);
        await LoadCart();
    }
    

    private async Task LoadCart()
    {
        if ((await CartService.GetCartItems()).Count == 0)
        {
            _message = "Your cart is empty!";
            _cartProducts = new List<CartProductResponseDto>();
        }
        else
        {
            _cartProducts = await CartService.GetCartProducts();
        }
    }

    private async Task UpdateQuantity(ChangeEventArgs e, CartProductResponseDto product)
    {
        product.Quantity = int.Parse(e.Value.ToString());
        if (product.Quantity < 1)
            product.Quantity = 1;
        await CartService.UpdateQuantity(product);
    }
}