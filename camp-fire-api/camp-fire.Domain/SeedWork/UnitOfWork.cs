using System.Threading.Tasks;
using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.SeedWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly CampFireDBContext _context;

    public UnitOfWork(CampFireDBContext context)
    {
        _context = context;
    }

    public IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return new Repository<T>(_context);
    }


    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
