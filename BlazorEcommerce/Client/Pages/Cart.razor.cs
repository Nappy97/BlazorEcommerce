using BlazorEcommerce.Shared.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class Cart
{
    [Inject] private ICartService CartService { get; set; }
    [Inject] private IOrderService OrderService { get; set; }

    private List<CartProductResponseDto> _cartProducts = null;
    private string _message = "Loading cart...";
    private bool _isOrderPlaced = false;

    protected override async Task OnInitializedAsync()
    {
        _isOrderPlaced = false;
        await LoadCart();
    }

    private async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        await CartService.RemoveProductFromCart(productId, productTypeId);
        await LoadCart();
    }


    private async Task LoadCart()
    {
        await CartService.GetCartItemsCount();
        _cartProducts = await CartService.GetCartProducts();
        if (_cartProducts == null || _cartProducts.Count == 0)
        {
            _message = "Your cart is empty!";
        }
    }

    private async Task UpdateQuantity(ChangeEventArgs e, CartProductResponseDto product)
    {
        product.Quantity = int.Parse(e.Value.ToString());
        if (product.Quantity < 1)
            product.Quantity = 1;
        await CartService.UpdateQuantity(product);
    }

    // 장바구니에서 주문하기
    private async Task PlaceOrder()
    {
        await OrderService.PlaceOrder();
        await CartService.GetCartItemsCount();
        _isOrderPlaced = true;
    }
}