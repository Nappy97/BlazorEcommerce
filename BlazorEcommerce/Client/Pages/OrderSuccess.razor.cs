using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages;

public partial class OrderSuccess
{
    [Inject] ICartService CartService { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await CartService.GetCartItemsCount();
    }
}