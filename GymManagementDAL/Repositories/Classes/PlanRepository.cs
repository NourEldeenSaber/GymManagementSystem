using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;
        public PlanRepository(GymDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public int Add(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            return _dbContext.SaveChanges();
        }

        public int Delete(Plan plan)
        {
            _dbContext.Plans.Remove(plan);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAll() => _dbContext.Plans.ToList();

        public Plan? GetById(int id) => _dbContext.Plans.Find(id);

        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
