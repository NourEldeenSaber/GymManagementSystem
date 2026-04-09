
using GymManagementBLL.Services.Classes;
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory();
        IEnumerable<MemberForSessionViewModel> GetMembersSession(int id);
        public bool ToggleIsAttend(int memberId, int SessionId);
        public bool CreateBooking(CreateBookingViewModel model);

    }
}
