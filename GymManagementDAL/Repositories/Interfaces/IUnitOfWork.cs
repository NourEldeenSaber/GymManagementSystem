
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity , new();
        int SaveChanges();
    }
}
