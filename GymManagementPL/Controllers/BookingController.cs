using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        #region Create

        public IActionResult Create(int sessionId)
        {
            var Members = _bookingService.GetMembersForDropDown(sessionId);
            var membersSelectList = new SelectList(Members, "Id", "Name"); 
            ViewBag.Members = membersSelectList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBookingViewModel model)
        {
            
            var result = _bookingService.CreateBooking(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking Created Succefully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Create Booking";
            }
            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = model.SessionId });
        }

        #endregion
    }
}
