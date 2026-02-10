using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity , new()
    {

        // Inject GymDbContext via constructor to enable database operations using Dependency Injection
        private readonly GymDbContext _dbContext;
        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Adds a new entity to the database and commits the changes
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        // Removes the specified entity from the database and commits the changes
        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        // Retrieves all entities without tracking to improve read-only query performance
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null) {
            if (condition is null)
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }

        // Retrieves a single entity by its primary key value
        public TEntity? GetById(int Id) => _dbContext.Set<TEntity>().Find(Id);

        // Updates an existing entity and commits the changes to the database
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
