using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;

namespace SecondhandStore.Repository.BaseRepository;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    private readonly SecondhandStoreContext _dbContext;
    

    protected BaseRepository(SecondhandStoreContext dbContext)
    {
        _dbContext = dbContext;
    }
 
    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return _dbContext.Set<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting entity: {ex.Message}", ex);
        }
    }
    
    public async Task<TEntity?> GetById(string id)
    {
        try
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting entity: {ex.Message}", ex);
        }
    }
    
    public async Task<TEntity?> GetByIntId(int id)
    {
        try
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting entity: {ex.Message}", ex);
        }
    }


    public async Task Add(TEntity entity)
    {
        try
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding entity: {ex.Message}", ex);
        }
    }

    public async Task Update(TEntity entity)
    {
        try
        {            
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating entity: {ex.Message}", ex);
        }
    }

    public async Task Delete(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting entity: {ex.Message}", ex);
        }
    }
}