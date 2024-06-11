

using ProductManagementAPI.Entities.ViewModels;
using ProductManagementAPI.Entity.Models;


namespace ProductManagement.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        public List<City> GetCites();
        public Product AddEditProduct(ProductDetails productAddEdit);
        public List<ProductDetails> GetAllProducts(ProductListParams listParams);
        public ProductDetails GetProductDetail(int productId);
        public bool DeleteProduct(int productId);
        public int GetAllProductsCount(ProductListParams listParams);
        public List<Product> GetAllProducts();
    }
}
