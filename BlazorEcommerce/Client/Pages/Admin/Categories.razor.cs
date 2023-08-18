using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages.Admin;

[Authorize(Roles = "Admin")]
public partial class Categories : IDisposable
{
    [Inject] ICategoryService CategoryService { get; set; }

    Category _editingCategory = new();

    protected override async Task OnInitializedAsync()
    {
        await CategoryService.GetAdminCategories();
        CategoryService.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        CategoryService.OnChange -= StateHasChanged;
    }

    private void CreateNewCategory()
    {
        _editingCategory = CategoryService.CreateNewCategory();
    }

    private void EditCategory(Category category)
    {
        category.Editing = true;
        _editingCategory = category;
    }

    private async Task UpdateCategory()
    {
        if (_editingCategory.IsNew)
            await CategoryService.AddCategory(_editingCategory);
        else
            await CategoryService.UpdateCategory(_editingCategory);
        _editingCategory = null;
    }

    private async Task CancelEditing()
    {
        _editingCategory = new Category();
        await CategoryService.GetAdminCategories();
    }

    private async Task DeleteCategory(int categoryId)
    {
        await CategoryService.DeleteCategory(categoryId);
    }
    
}