using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;

namespace SecondhandStore.Repository.BaseRepository;

using System.Collections.Generic;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    private readonly SecondhandStoreContext _dbContext;

    protected BaseRepository(SecondhandStoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetById(string id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
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