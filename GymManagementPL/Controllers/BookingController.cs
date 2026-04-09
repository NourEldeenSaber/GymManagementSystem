using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class BookingController(IBookingService bookingService) : Controller
    {
        private readonly IBookingService _bookingService = bookingService;

        public IActionResult Index()
        {
            var sessions = _bookingService.GetAllSessionsWithTrainerAndCategory();
            return View(sessions);
        }

        public IActionResult GetMembersForUpcomingSession(int id)
        {
            var members = _bookingService.GetAllMembersForUpcomingSession(id);
            return View(members);
        }
    }
}
