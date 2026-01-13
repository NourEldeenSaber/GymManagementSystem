
namespace GymManagementDAL.Entities
{
    public class MemberSession : BaseEntity
    {
        #region Prop

        //BookingDate - CreatedAt Of Base

        public bool IsAttended{ get; set; }

        #endregion

        #region Member
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        #endregion

        #region Session

        public int SessionId { get; set; }

        public Session Session { get; set; } = null!;

        #endregion


    }
}
