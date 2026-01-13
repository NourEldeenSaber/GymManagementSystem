using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    public class MemberSessionRepository : IMemberSessionRepository
    {
        private readonly GymDbContext _dbContex;
        public MemberSessionRepository(GymDbContext dbContext)
        {
            _dbContex = dbContext;
        }

        public int Add(MemberSession memberSession)
        {
            _dbContex.MemberSessions.Add(memberSession);
            return _dbContex.SaveChanges();
        }

        public int Delete(MemberSession memberSession)
        {
            _dbContex.MemberSessions.Remove(memberSession);
            return _dbContex.SaveChanges();
        }

        public IEnumerable<MemberSession> GetAll() => _dbContex.MemberSessions.ToList();

        public MemberSession? GetById(int id) => _dbContex.MemberSessions.Find(id);
       

        public int Update(MemberSession memberSession)
        {
            _dbContex.MemberSessions.Update(memberSession);
            return _dbContex.SaveChanges();
        }
    }
}
