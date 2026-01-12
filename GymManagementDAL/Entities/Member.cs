
namespace GymManagementDAL.Entities
{
    internal class Member : GymUser
    {
        // JoinDate  == CreatedAt of baseEntity
        public string Photo { get; set; }
        
        #region Relations

        #region Member - HealthRecord Relation

        public HealthRecord HealthRecord { get; set; } = null!;

        #endregion

        #region Member - MemberShip

        public ICollection<MemberShip> MemberShips { get; set; } = null!;

        #endregion

        #region Member - MemberSession

        public ICollection<MemberSession> MemberSessions { get; set; } = null!;

        #endregion

        #endregion
    }
}
