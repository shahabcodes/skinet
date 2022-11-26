using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
         Task<Product> GetProductByIdAsync(int Id);
         Task<IReadOnlyList<Product>> GetProductsAsync();
         Task<IReadOnlyList<ProductBrands>> GetProductBrandsAsync();
         Task<IReadOnlyList<ProductTypes>> GetProductTypesAsync();
    }
}