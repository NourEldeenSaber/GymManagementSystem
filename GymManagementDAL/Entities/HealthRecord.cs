

namespace GymManagementDAL.Entities
{
    // 1-1 Relationship with member [Shared PK]
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }

        

    }
}
