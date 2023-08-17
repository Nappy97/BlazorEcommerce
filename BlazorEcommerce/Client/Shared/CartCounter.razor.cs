using BlazorEcommerce.Client.Services.CartService;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Internal;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Shared;

public partial class CartCounter : IDisposable
{
    [Inject] private ICartService CartService { get; set; }
    [Inject] private ISyncLocalStorageService LocalStorage { get; set; }

    private int GetCartItemsCount()
    {
        var count = LocalStorage.GetItem<int>("cartItemsCount");
        return count;
    }

    protected override void OnInitialized()
    {
        CartService.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        CartService.OnChange -= StateHasChanged;
    }
}