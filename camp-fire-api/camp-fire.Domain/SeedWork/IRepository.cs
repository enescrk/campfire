using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.SeedWork;

public interface IRepository<T> where T : IBaseEntity
{
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    T FindOne(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(int id);
    T GetById(int id);
    Task CreateAsync(T entity);
    Task BulkCreateAsync(List<T> entities);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    void HardDelete(T entity);
}
