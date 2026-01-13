using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        // GetAll
        IEnumerable<Member> GetAll();

        // GetById
        Member? GetById(int id);

        // Add
        int Add(Member member);

        // Update
        int Update(Member member);

        // Delete
        int Delete(int id);
    }
}
