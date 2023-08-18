using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Shared;

public partial class ShopNavMenu : IDisposable
{
    [Inject] private ICategoryService CategoryService { get; set; }
    
    private bool collapseNavMenu = true;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    protected override async Task OnInitializedAsync()
    {
        await CategoryService.GetCategories();
        CategoryService.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        CategoryService.OnChange -= StateHasChanged;
    }
}