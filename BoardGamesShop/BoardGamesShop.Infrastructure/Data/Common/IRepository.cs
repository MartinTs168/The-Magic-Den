namespace BoardGamesShop.Infrastructure.Data.Common;

public interface IRepository
{
    IQueryable<T> All<T>() where T : class;

    IQueryable<T> AllReadOnly<T>() where T : class;

    Task AddAsync<T>(T entity) where T : class;
    
    Task<T?> GetByIdAsync<T>(object id) where T : class;

    Task<int> SaveChangesAsync();
    
    Task DeleteAsync<T>(object id) where T : class;
}