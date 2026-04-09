using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class BookingRepository : GenericRepository<MemberSession>, IBookingRepository
    {
        private readonly GymDbContext _dbContext;

        public BookingRepository(GymDbContext dbContext) :base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public IEnumerable<MemberSession> GetSessionById(int sessionId)
        {
            return _dbContext.MemberSessions.Where(ms => ms.SessionId == sessionId)
                                            .Include(ms => ms.Member)
                                            .ToList();
        }
    }
}
