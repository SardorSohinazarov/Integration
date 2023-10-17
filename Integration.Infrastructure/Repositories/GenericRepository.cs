using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Infrastructure.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly AppDbContext context;
    public GenericRepository(AppDbContext context) =>
        this.context = context;

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        var entityEntry = await this.context.
            AddAsync<TEntity>(entity);
        await this.context.SaveChangesAsync();

        return entityEntry.Entity;
    }
    public IQueryable<TEntity> SelectAll() =>
        this.context.Set<TEntity>();
    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = this.context
            .Update<TEntity>(entity);

        await this.SaveChangesAsync();

        return entityEntry.Entity;
    }
    public async ValueTask<TEntity> DeleteAsync(TEntity entity)
    {
        var entityEntry = this.context
            .Set<TEntity>()
            .Remove(entity);

        await this.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async ValueTask<int> SaveChangesAsync() =>
        await this.context.SaveChangesAsync();

    public async ValueTask<TEntity> SelectByIdAsync(int id) =>
        await this.context.Set<TEntity>().FindAsync(id);
}
