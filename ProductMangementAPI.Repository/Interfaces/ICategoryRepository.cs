
using ProductManagementAPI.Entities.ViewModels;
using ProductManagementAPI.Entity.Models;

namespace ProductManagement.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Category AddEditCategory(CategoryDetails categoryAddEdit);
        public List<CategoryDetails> GetAllCategories(int page, int pageSize);
        public bool DeleteCategory(int categoryId);
        public CategoryDetails GetCategoryDetails(int categoryId);
        public int GetAllCategoriesCount();
        public List<CategoryDetails> GetCategories();

    }
}
