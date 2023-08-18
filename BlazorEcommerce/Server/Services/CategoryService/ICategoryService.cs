using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;
using BlazorEcommerce.Shared.Response;

namespace BlazorEcommerce.Server.Services.CategoryService;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>> GetCategories();
    Task<ServiceResponse<List<Category>>> GetAdminCategories();
    Task<ServiceResponse<List<Category>>> AddCategory(Category category);
    Task<ServiceResponse<List<Category>>> UpdateCategory(Category category);
    Task<ServiceResponse<List<Category>>> DeleteCategory(int id);
}