using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Infrastructure.Data.Common;

public class Repository : IRepository
{
    private readonly DbContext _context;
    private readonly bool _allowNoTracking;

    public Repository(ApplicationDbContext context, bool allowNoTracking = true)
    {
        _context = context;
        _allowNoTracking = allowNoTracking;
    }

    private DbSet<T> DbSet<T>() where T : class
    {
        return _context.Set<T>();
    }
    
    
    public IQueryable<T> All<T>() where T : class
    {
        return DbSet<T>();
    }

    public IQueryable<T> AllReadOnly<T>() where T : class
    {
        return _allowNoTracking ? DbSet<T>().AsNoTracking() : DbSet<T>();
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        await DbSet<T>().AddAsync(entity);
    }

    public async Task<T?> GetByIdAsync<T>(object id) where T : class
    {
        return await DbSet<T>().FindAsync(id);
    }

    public async Task<T?> GetByIdAsync<T>(object id1, object id2) where T : class
    {
        return await DbSet<T>().FindAsync(id1, id2);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(object id) where T : class
    {
        T? entity = await GetByIdAsync<T>(id);

        if (entity != null)
        {
            DbSet<T>().Remove(entity);
        }
    }

    public async Task DeleteAsync<T>(object id1, object id2) where T : class
    {
        var entity = await DbSet<T>().FindAsync(id1, id2);
        
        if (entity != null)
        {
            DbSet<T>().Remove(entity);
        }
    }
}