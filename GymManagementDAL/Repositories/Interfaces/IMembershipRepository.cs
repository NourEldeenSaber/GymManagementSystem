using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IMembershipRepository : IGenericRepository<MemberShip>
    {
        IEnumerable<MemberShip> GetAllMembershipsWithMembersAndPlans(Func<MemberShip,bool>? filter = null);
    }
}
