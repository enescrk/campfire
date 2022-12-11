using System.Threading.Tasks;
using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.SeedWork;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync();
    int SaveChanges();
}
    