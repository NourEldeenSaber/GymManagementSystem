using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        IEnumerable<Trainer> GetAll();
        Trainer? GetById(int id);
        int Add(Trainer trainer);
        int Update(Trainer trainer);
        int Delete(Trainer trainer);
    }
}
