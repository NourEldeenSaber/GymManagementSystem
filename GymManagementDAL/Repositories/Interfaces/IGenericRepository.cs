using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity , new() /*not accept the abstract class */
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(int Id);
        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);

    }
}
