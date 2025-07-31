using Microsoft.EntityFrameworkCore;
using System;
using Worker.Core.Models;
using Worker.Infrastructure.Data;

namespace Worker.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : DbEntity
{
    Task<RepositoryServiceResult<TEntity>> AddAsync(TEntity entity);
    Task<RepositoryServiceResult<TEntity>> DeleteAsync(Guid id);
    Task<RepositoryServiceResult<List<TEntity>>> GetAllAsync(Func<TEntity, bool> predicate);
    Task<RepositoryServiceResult<List<TEntity>>> GetAllAsync();
    Task<RepositoryServiceResult<TEntity>> GetByIdAsync(Guid id);
    Task<RepositoryServiceResult<TEntity>> UpdateAsync(TEntity entity);
    Task<RepositoryServiceResult<TEntity>> UpdateOrInsert(TEntity entity);
}

public abstract class Repository<TEntity>(WorkerDbContext context) : IRepository<TEntity> where TEntity : DbEntity
{
    protected WorkerDbContext Context { get; } = context;
    protected DbSet<TEntity> Table => Context.Set<TEntity>();


    protected static RepositoryServiceResult<TResult> IdResultFound<TResult>(TResult result, Guid id) 
        => new(result, $"Result found for id {id}.");

    protected static RepositoryServiceResult<TResult> ResultFound<TResult>(TResult result)
        => new(result, "Result found.");

    protected static RepositoryServiceResult<TResult> EntityCreated<TResult>(TResult entity)
        => new(entity, "Entity created successfully.");

    protected static RepositoryServiceResult<TResult> EntityUpdated<TResult>(TResult entity)
        => new(entity, "Entity updated successfully.");

    protected static RepositoryServiceResult<TResult> EntityRemoved<TResult>(TResult entity)
        => new(entity, "Entity removed successfully.");

    protected static RepositoryServiceResult<TResult> ContainsId<TResult>()
        => new($"Cannot add to the database if id is filled.");

    protected static RepositoryServiceResult<TResult> NoResult<TResult>(Guid id) 
        => new($"No results found for id {id}.");

    protected static RepositoryServiceResult<TResult> NoResults<TResult>()
        => new("No results found.");

    protected static RepositoryServiceResult<TResult> EmptyParameters<TResult>(string[] parameters) 
        => new($"{string.Join(',', parameters)} cannot be empty.");

    protected static RepositoryServiceResult<TResult> Exception<TResult>(Exception exception)
        => new("An unexpected error occurred.", exception);

    public virtual async Task<RepositoryServiceResult<TEntity>> GetByIdAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
                return EmptyParameters<TEntity>([nameof(id)]);

            var entity = await Table.FindAsync(id);
            if (entity is null)
                return NoResult<TEntity>(id);

            return IdResultFound(entity, id);
        }
        catch (Exception ex)
        {
            return Exception<TEntity>(ex);
        }
    }

    public virtual async Task<RepositoryServiceResult<List<TEntity>>> GetAllAsync(Func<TEntity, bool> predicate)
    {
        try
        {
            var entities = Table.Where(predicate).ToList();
            if (entities.Count == 0)
                return await Task.FromResult(NoResults<List<TEntity>>());

            return await Task.FromResult(ResultFound(entities));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Exception<List<TEntity>>(ex));
        }
    }

    public virtual async Task<RepositoryServiceResult<List<TEntity>>> GetAllAsync()
    {

        try
        {
            var entities = await Table.ToListAsync();
            if (entities.Count == 0)
                return await Task.FromResult(NoResults<List<TEntity>>());

            return await Task.FromResult(ResultFound(entities));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Exception<List<TEntity>>(ex));
        }
    }

    public virtual async Task<RepositoryServiceResult<TEntity>> UpdateOrInsert(TEntity entity)
    {
        if (entity is null)
            return await Task.FromResult(EmptyParameters<TEntity>([nameof(entity)]));

        if (entity.Id == Guid.Empty) // If the entity does not have an ID, it is considered a new entity
            return await AddAsync(entity);
        else 
            return await UpdateAsync(entity);
    }

    public virtual async Task<RepositoryServiceResult<TEntity>> AddAsync(TEntity entity)
    {
        if (entity is null)
            return await Task.FromResult(EmptyParameters<TEntity>([nameof(entity)]));

        try
        {
            if (entity.Id != Guid.Empty)
                return ContainsId<TEntity>();

            Table.Add(entity);
            await Context.SaveChangesAsync();

            return await Task.FromResult(EntityCreated(entity));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Exception<TEntity>(ex));
        }
    }

    public virtual async Task<RepositoryServiceResult<TEntity>> UpdateAsync(TEntity entity)
    {
        if (entity is null)
            return await Task.FromResult(EmptyParameters<TEntity>([nameof(entity)]));

        try
        {
            if (entity.Id == Guid.Empty)
                return EmptyParameters<TEntity>([nameof(entity.Id)]);

            Table.Update(entity);
            await Context.SaveChangesAsync();

            return await Task.FromResult(EntityUpdated(entity));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Exception<TEntity>(ex));
        }
    }

    public virtual async Task<RepositoryServiceResult<TEntity>> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            return await Task.FromResult(EmptyParameters<TEntity>([nameof(id)]));

        try
        {
            var findResult = await GetByIdAsync(id);
            if (!findResult.IsSuccess)
                return await Task.FromResult(findResult);

            Table.Remove(findResult.Result);
            await Context.SaveChangesAsync();

            return await Task.FromResult(EntityRemoved(findResult.Result));
        }
    }
}