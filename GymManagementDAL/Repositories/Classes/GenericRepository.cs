using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {

        // Inject GymDbContext via constructor to enable database operations using Dependency Injection
        private readonly GymDbContext _dbContext;
        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Adds a new entity to the database 
        public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);


        // Removes the specified entity from the database
        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);



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
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
    
    }
}
