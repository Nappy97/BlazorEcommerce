using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorEcommerce.Client.Shared;

public partial class Search
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IProductService ProductService { get; set; }

    private string SearchText = string.Empty;
    private List<string> Suggestions = new();
    protected ElementReference SearchInput;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SearchInput.FocusAsync();
        }
    }

    public void SearchProducts()
    {
        NavigationManager.NavigateTo($"search/{SearchText}");
    }

    public async Task HandleSearch(KeyboardEventArgs args)
    {
        if (args.Key is null or "Enter")
        {
            SearchProducts();
        }
        else if (SearchText.Length > 1)
        {
            Suggestions = await ProductService.GetProductSearchSuggestions(SearchText);
        }
    }
}