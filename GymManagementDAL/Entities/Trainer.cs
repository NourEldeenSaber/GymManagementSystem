using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities
{
    internal class Trainer : GymUser
    {
        // HireDate -> Created At of Base
        public Specialties Specialties { get; set; }

        public ICollection<Session> TrainerSession { get; set; } = null!;
    }
}
