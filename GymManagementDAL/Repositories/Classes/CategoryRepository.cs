using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GymDbContext _dbContext;
        public CategoryRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Category category)
        {
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null)
                return 0;

            _dbContext.Categories.Remove(category);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll() => _dbContext.Categories.ToList();
        
        public Category? GetById(int id) => _dbContext.Categories.Find(id);

        public int Update(Category category)
        {
            _dbContext.Categories.Update(category);
            return _dbContext.SaveChanges();
        }
    }
}
