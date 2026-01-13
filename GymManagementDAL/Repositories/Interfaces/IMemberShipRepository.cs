using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IMemberShipRepository
    {
        // GetAll
        IEnumerable<MemberShip> GetAll();

        // GetById
        MemberShip? GetById(int id);

        // Add
        int Add(MemberShip memberShip);

        // Update
        int Update(MemberShip memberShip);

        // Delete
        int Delete(int id);
    }
}
