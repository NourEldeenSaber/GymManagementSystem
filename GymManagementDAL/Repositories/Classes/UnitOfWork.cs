using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContext _dbContext;

        public UnitOfWork(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out var repo))
                return (IGenericRepository<TEntity>) repo;
            var newRepo = new GenericRepository<TEntity>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }


    }
}
