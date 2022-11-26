using Core.Entities;
using Core.Interfaces;
using Dapper;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            using (var db = _context.ConnectSkinet())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                var data = await db.QueryFirstOrDefaultAsync<Product>
                ("[dbo].[GetProductById]", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            using (var db = _context.ConnectSkinet())
            {
                DynamicParameters parameters = new DynamicParameters();
                var data = await db.QueryAsync<Product>
                ("[dbo].[GetProducts]", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task<IReadOnlyList<ProductBrands>> GetProductBrandsAsync()
        {
            using (var db = _context.ConnectSkinet())
            {
                DynamicParameters parameters = new DynamicParameters();
                var data = await db.QueryAsync<ProductBrands>
                ("[dbo].[GetProductBrands]", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task<IReadOnlyList<ProductTypes>> GetProductTypesAsync()
        {
            using (var db = _context.ConnectSkinet())
            {
                DynamicParameters parameters = new DynamicParameters();
                var data = await db.QueryAsync<ProductTypes>
                ("[dbo].[GetProductTypes]", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure);
                return data.ToList();
            }
        }
    }
}