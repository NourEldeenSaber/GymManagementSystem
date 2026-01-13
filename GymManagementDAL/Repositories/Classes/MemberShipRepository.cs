using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    public class MemberShipRepository : IMemberShipRepository
    {
        private readonly GymDbContext _dbContext;

        public MemberShipRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(MemberShip memberShip)
        {
            _dbContext.MemberShips.Add(memberShip);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var MemberShip = _dbContext.MemberShips.Find(id);
            if (MemberShip is null) return 0;

            _dbContext.MemberShips.Remove(MemberShip);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<MemberShip> GetAll() => _dbContext.MemberShips.ToList();

        public MemberShip? GetById(int id) => _dbContext.MemberShips.Find(id);
        

        public int Update(MemberShip memberShip)
        {
            _dbContext.MemberShips.Update(memberShip);
            return _dbContext.SaveChanges();
        }
    }
}
