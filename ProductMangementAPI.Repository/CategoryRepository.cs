using ProductManagement.Repositories.Interfaces;
using ProductManagementAPI.Entities.ViewModels;
using ProductManagementAPI.Entity.DataContext;
using ProductManagementAPI.Entity.Models;

namespace ProductManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Add And Edit category
        /// <summary>
        /// Add and Edit Category
        /// </summary>
        /// <param name="categoryAddEdit"></param>
        /// <returns></returns>
        public bool AddEditCategory(CategoryDetails categoryAddEdit)
        {
            try
            {
                Category? category = (categoryAddEdit.CategoryId != null) ? _context.Categories.FirstOrDefault(e => e.CategoryId == categoryAddEdit.CategoryId) : new Category();
                if(category != null)
                {
                    category.CategoryName = categoryAddEdit.CategoryName;
                    category.Sequence = (int)categoryAddEdit.Sequence;
                    InsertOrUpdateCategory(category);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Generic Function for Insert or Update Category
        /// <summary>
        /// Generic Function for Insert or Update Category
        /// </summary>
        /// <param name="category"></param>
        public void InsertOrUpdateCategory(Category category)
        {
            try
            {
                if (category.CategoryId != default)
                {
                    category.ModifiedDate = DateTime.Now;
                    _context.Categories.Update(category);
                }
                else
                {
                    category.CreatedDate = DateTime.Now;
                    _context.Categories.Add(category);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Delete Category
        /// <summary>
        /// Delete category only if no products are avilable in this category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId)
        {
            try
            {
                if (_context.Products.Any(e => e.CategoryId == categoryId))
                {
                    return false;
                }
                else
                {
                    Category? category = _context.Categories.FirstOrDefault(e => e.CategoryId == categoryId);
                    if (category != null)
                    {
                        category.DeletedAt = DateTime.Now;
                        InsertOrUpdateCategory(category);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get All Category for displaing category List
        /// <summary>
        /// Get All Category for displaing category List
        /// </summary>
        /// <returns></returns>
        public List<CategoryDetails> GetAllCategories(int page, int pageSize)
        {
            try
            {
                List<CategoryDetails> data = GetCategories();
                List<CategoryDetails> paginatedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return paginatedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Gets all categories count.
        /// <summary>
        /// Gets all categories count.
        /// </summary>
        /// <returns></returns>
        public int GetAllCategoriesCount()
        {
            try
            {
                List<CategoryDetails> data = GetCategories();
                return data.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetCategories
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<CategoryDetails> GetCategories()
        {
            try
            {
                List<CategoryDetails> data = (from c in _context.Categories
                                              where !c.DeletedAt.HasValue
                                              orderby c.Sequence, c.CategoryId ascending
                                              select new CategoryDetails
                                              {
                                                  CategoryId = c.CategoryId,
                                                  CategoryName = c.CategoryName,
                                                  TotalProducts = _context.Products.Where(x => x.CategoryId == c.CategoryId).Count(),
                                                  Sequence = c.Sequence,
                                              }).ToList();
               
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetCategoryDetails for edit category
        /// <summary>
        /// GetCategoryDetails for edit category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public CategoryDetails GetCategoryDetails(int CategoryId)
        {
            try
            {
                Category categoryDetail = _context.Categories.Where(e => e.CategoryId == CategoryId).First();
                CategoryDetails data = new CategoryDetails
                {
                    CategoryId = categoryDetail.CategoryId,
                    CategoryName = categoryDetail.CategoryName,
                    Sequence = categoryDetail.Sequence
                };
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
