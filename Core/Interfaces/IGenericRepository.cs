using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(string storedProcedure, Dictionary<string, object> dictionary);
        Task<IReadOnlyList<T>?> ListAllAsync(string storedProcedure, Dictionary<string, object> dictionary);
    }
}