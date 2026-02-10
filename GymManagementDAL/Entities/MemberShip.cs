namespace GymManagementDAL.Entities
{
    public class MemberShip : BaseEntity
    {

        #region prop

        //StartDate -CreatedAt Of BaseEntity [ in configuration we just rename the column]

        public DateTime EndDate { get; set; }

        //ReadOnly Property
        public string Status
        {
            get {
                if (EndDate >= DateTime.Now)
                    return "Expierd";
                else
                    return "Active";
            }
        }

        #endregion

        #region Relations

        #region Member

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        #endregion

        #region Plan

        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;

        #endregion

        #endregion

    }
}
