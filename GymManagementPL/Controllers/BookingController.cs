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
            var members = _bookingService.GetMembersSession(id);
            return View(members);
        }

        public IActionResult GetMembersForOngoingSession(int id)
        {
            var members = _bookingService.GetMembersSession(id);
            return View(members);
        }

        [HttpPost]
        public IActionResult MarkAsAttended(int memberId, int sessionId)
        {
            if (memberId < 0 && sessionId < 0)
            {
                TempData["ErrorMessage"] = "Invalid to Attend";
                return RedirectToAction(nameof(Index));
            }
            var res = _bookingService.ToggleIsAttend(memberId, sessionId);
            if (res)
            {
                TempData["SuccessMessage"] = "Active Attended ";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Attend";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
