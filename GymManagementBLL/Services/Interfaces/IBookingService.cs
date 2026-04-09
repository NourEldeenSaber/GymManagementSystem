
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory();
        IEnumerable<MemberForSessionViewModel> GetAllMembersForUpcomingSession(int id);
    }
}
