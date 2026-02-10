using GymManagementBLL.ViewModels.MemberViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);
    }
}
