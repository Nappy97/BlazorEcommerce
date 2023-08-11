using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Data;
using BlazorEcommerce.Shared.Response;

namespace BlazorEcommerce.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Category>>> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return new ServiceResponse<List<Category>>
        {
            Data = categories
        };
    }
}