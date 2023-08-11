using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;
using BlazorEcommerce.Shared.Response;

namespace BlazorEcommerce.Server.Services.CategoryService;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>> GetCategories();
}