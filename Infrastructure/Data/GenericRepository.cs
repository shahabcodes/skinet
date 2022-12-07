using Core.Interfaces;
using Dapper;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ILoggerManager _logger;
        private StoreContext _context;

        public GenericRepository(StoreContext context, ILoggerManager logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<T?> GetByIdAsync(string storedProcedure, Dictionary<string, object> dictionary)
        {
            try
            {
                using (var db = _context.ConnectSkinet())
                {
                    var data = await db.QueryFirstOrDefaultAsync<T>
                    (storedProcedure, param: new DynamicParameters(dictionary),
                    commandType: System.Data.CommandType.StoredProcedure);
                    return data;
                }
            }
            catch (Exception ex)
            {                
                _logger.LogAll(ex);
                return null;
            }
        }

        public async Task<IReadOnlyList<T>?> ListAllAsync(string storedProcedure, Dictionary<string, object> dictionary)
        {
            try
            {
                using (var db = _context.ConnectSkinet())
                {
                    var data = await db.QueryAsync<T>
                    (storedProcedure, param: new DynamicParameters(dictionary),
                    commandType: System.Data.CommandType.StoredProcedure);
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogAll(ex);
                return null;
            }                
        }

    }
}