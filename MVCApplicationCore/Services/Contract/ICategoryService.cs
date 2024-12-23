using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCApplicationCore.Data;
using MVCApplicationCore.Models;
using System.Drawing.Printing;

namespace MVCApplicationCore.Services.Contract
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category? GetCategory(int id);
        string AddCategory(Category category,IFormFile file);
        string RemoveCategory(int id);
        string ModifyCategory(Category category);
        int TotalCategories();
        IEnumerable<Category> GetPaginatedCategories(int page, int pageSize);


    }
}
