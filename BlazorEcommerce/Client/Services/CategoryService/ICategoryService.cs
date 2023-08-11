using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;

namespace BlazorEcommerce.Client.Services.CategoryService;

public interface ICategoryService
{
    List<Category> Categories { get; set; }

    Task GetCategories();
}