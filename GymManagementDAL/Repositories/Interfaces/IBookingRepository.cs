using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IBookingRepository  : IGenericRepository<MemberSession> 
    {
        IEnumerable<MemberSession> GetSessionById(int sessionId);

        public MemberSession? GetSessionByMemberIdAndSessionId(int memberId, int sessionId);
    }
}
