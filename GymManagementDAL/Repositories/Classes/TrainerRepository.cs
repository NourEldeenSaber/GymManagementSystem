using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _dbcontext;
        public TrainerRepository(GymDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public int Add(Trainer trainer)
        {
            _dbcontext.Trainers.Add(trainer);
            return _dbcontext.SaveChanges();
        }

        public int Delete(Trainer trainer)
        {
            _dbcontext.Trainers.Remove(trainer);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll()
            => _dbcontext.Trainers.ToList();
        

        public Trainer? GetById(int id)
           => _dbcontext.Trainers.Find(id);
        

        public int Update(Trainer trainer)
        {
            _dbcontext.Trainers.Update(trainer);
            return _dbcontext.SaveChanges();
        }
    }
}
