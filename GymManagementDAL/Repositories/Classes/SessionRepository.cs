using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        //CLR Will Inject Object From GymDbContext
        public SessionRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Session session)
        {
            _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var session = _dbContext.Sessions.Find(id);
            if (session is null)
                return 0;

            _dbContext.Sessions.Remove(session);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAll() => _dbContext.Sessions.ToList();

        public Session? GetById(int id) => _dbContext.Sessions.Find(id);

        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
