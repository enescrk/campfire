using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using camp_fire.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace camp_fire.Domain.SeedWork;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    #region Settings

    private readonly CampFireDBContext _context;
    private DbSet<T> _entity;

    public Repository(CampFireDBContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    protected DbSet<T> Table => _entity ?? (_entity = _context.Set<T>());

    #endregion

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return Table.Where(predicate).Where(x => !x.IsDeleted).AsQueryable();
    }

    public T FindOne(Expression<Func<T, bool>> predicate)
    {
        return Table.FirstOrDefault(predicate);
    }

    public IQueryable<T> GetAll()
    {
        return Table.Where(x => !x.IsDeleted).AsQueryable();
    }

    public T GetById(int id)
    {
        var result = Table.Find(id);

        return result == null ? null : result.IsDeleted ? null : result;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var result = await Table.FindAsync(id);

        return result == null ? null : result.IsDeleted ? null : result;
    }

    public async Task CreateAsync(T entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.IsDeleted = false;
        await Table.AddAsync(entity);
    }

    public virtual async Task BulkCreateAsync(List<T> entities)
    {
        foreach (var item in entities)
        {
            var added = _context.Entry(item);
            added.State = EntityState.Added;

            await Table.AddAsync(item);
        }

        await _context.SaveChangesAsync();
    }

    public void Create(T entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.IsDeleted = false;
        Table.Add(entity);
    }

    public void Delete(T entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        entity.IsDeleted = true;
    }

    public void HardDelete(T entity)
    {
        Table.Remove(entity);
    }

    public void Update(T entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        Table.Update(entity);
    }
}
