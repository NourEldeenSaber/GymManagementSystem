
namespace GymManagementDAL.Entities
{
    internal class Member : GymUser
    {
        // JoinDate  == CreatedAt of baseEntity
        public string Photo { get; set; }

        #region Member - HealthRecord Relation

        public HealthRecord HealthRecord { get; set; } = null!;

        #endregion

    }
}
