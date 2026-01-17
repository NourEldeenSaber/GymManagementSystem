using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        IEnumerable<Plan> GetAll();
        Plan? GetById(int id);
        int Update (Plan plan);
    }
}
