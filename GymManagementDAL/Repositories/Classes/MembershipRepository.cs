using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class MembershipRepository : GenericRepository<MemberShip>, IMembershipRepository
    {
        private readonly GymDbContext _dbContext;

        public MembershipRepository(GymDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MemberShip> GetAllMembershipsWithMembersAndPlans(Func<MemberShip, bool>? filter = null)
        {
            var memberShips = _dbContext.MemberShips.Include(m => m.Member)
                                                    .Include(m => m.Plan)
                                                    .Where(filter ?? ( _ => true));
            return memberShips;
        }
    }
}
