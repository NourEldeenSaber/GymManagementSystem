using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IMemberSessionRepository
    {
        IEnumerable<MemberSession> GetAll();
        MemberSession? GetById(int id);
        int Add(MemberSession memberSession);
        int Update(MemberSession memberSession);
        int Delete(MemberSession memberSession);
    }
}
