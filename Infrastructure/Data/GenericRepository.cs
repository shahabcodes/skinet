using Core.Interfaces;
using Dapper;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(string storedProcedure, Dictionary<string, object> dictionary)
        {
            using (var db = _context.ConnectSkinet())
            {
                var data = await db.QueryFirstOrDefaultAsync<T>
                (storedProcedure, param: new DynamicParameters(dictionary),
                commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(string storedProcedure, Dictionary<string, object> dictionary)
        {
            using (var db = _context.ConnectSkinet())
            {
                var data = await db.QueryAsync<T>
                (storedProcedure, param: new DynamicParameters(dictionary),
                commandType: System.Data.CommandType.StoredProcedure);
                return data.ToList();
            }
        }
    }
}