using GymManagementBLL.ViewModels.MembershipViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMembershipService
    {
        IEnumerable<MembershipViewModel> GetAllMemberShips();

        IEnumerable<PlanForSelectListViewModel> GetPlansForDropDown();
        IEnumerable<MemberForSelectListViewModel> GetMemberForDropDown();
        bool CreateMembership(CreateMembershipViewModel model);

        bool DeleteMembership(int MemberId);


    }
}
