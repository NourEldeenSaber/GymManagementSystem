
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using System.Text.Json;

namespace GymManagementDAL.Data.DataSeed
{
    public static class GymDbContextSeeding
    {

        public static bool SeedData(GymDbContext context)
        {
            try
            {
                var HasPlans = context.Plans.Any();
                var HasCategories = context.Categories.Any();

                if (HasCategories && HasPlans) return false;

                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Plan>("plans.json");
                    if (Plans.Any())
                        context.Plans.AddRange(Plans);
                }

                if (!HasCategories)
                {
                    var Categories = LoadDataFromJsonFile<Category>("categories.json");
                    if (Categories.Any())
                        context.Categories.AddRange(Categories);
                }

                return context.SaveChanges() > 0;
            }
            catch (Exception ex) {
                Console.WriteLine($"Seeding Faild! {ex} ");
                return false;
            }
        }

        private static List<T> LoadDataFromJsonFile<T>(string FileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FileName);
            if (File.Exists(FilePath)) throw new FileNotFoundException();

            string Data = File.ReadAllText(FilePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<List<T>>(Data, options) ?? new List<T>();   
        }
    }
}
