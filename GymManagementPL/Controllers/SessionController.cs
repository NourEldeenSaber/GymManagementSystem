using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #region Get All Sessions
        
        public IActionResult Index()
        {
            var Sessions = _sessionService.GetAllSession();
            return View(Sessions);
        }

        #endregion

        #region Session Details

        public IActionResult Details(int id) { 
            
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }

            var session =  _sessionService.GetSessionById(id);
            if(session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        #endregion

        #region Session Create

        public IActionResult Create()
        {
            LoadDropDowns();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSessionViewModel CreatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDowns();
                return View(CreatedSession);
            }
            var result = _sessionService.CreateSession(CreatedSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                LoadDropDowns();
                TempData["ErrorMessage"] = "Failed To Create Session";
                return View(CreatedSession);
            }
        }

        #endregion

        #region Helper Methods
        
        private void LoadDropDowns()
        {
            var Trainers = _sessionService.GetTrainerForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");

            var Categories = _sessionService.GetCategoryForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        }

        #endregion
    }
}
