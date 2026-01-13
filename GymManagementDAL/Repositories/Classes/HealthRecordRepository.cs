using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly GymDbContext _dbContext;

        public HealthRecordRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;   
        }

        public int Add(HealthRecord healthRecord)
        {
            _dbContext.HealthRecords.Add(healthRecord);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
           var healthRecord = _dbContext.HealthRecords.Find(id);
           if(healthRecord is null) return 0;

            _dbContext.HealthRecords.Remove(healthRecord);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<HealthRecord> GetAll() => _dbContext.HealthRecords.ToList();

        public HealthRecord? GetById(int id) => _dbContext.HealthRecords.Find(id);

        public int Update(HealthRecord healthRecord)
        {
            _dbContext.HealthRecords.Update(healthRecord);
            return _dbContext.SaveChanges();
        }
    }
}
