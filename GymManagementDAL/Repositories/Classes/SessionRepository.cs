

using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _dbContext.Sessions.Include(X=> X.SessionTrainer)
                                      .Include(X=>X.SessionCategory)
                                      .ToList();
        }

        public int GetCountOfBookSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(X=>X.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions.Include(Q=>Q.SessionTrainer)
                                      .Include(Q=>Q.SessionCategory)  
                                      .FirstOrDefault(X=>X.Id == sessionId);  
        }
    }
}
