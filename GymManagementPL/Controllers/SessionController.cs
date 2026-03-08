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
            LoadDropDownsForCategories();
            LoadDropDownsForTrainers();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSessionViewModel CreatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownsForCategories();
                LoadDropDownsForTrainers();
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
                LoadDropDownsForCategories();
                LoadDropDownsForTrainers();
                TempData["ErrorMessage"] = "Failed To Create Session";
                return View(CreatedSession);
            }
        }

        #endregion

        #region Session Edit
        
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionToUpdate(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Cannot be Updated";
                return RedirectToAction(nameof(Index));
            }
            LoadDropDownsForTrainers();
            return View(session);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id ,UpdateSessionViewModel updatedSession)
        {
            if (!ModelState.IsValid) {
                LoadDropDownsForTrainers();
                return View(updatedSession);
            }
            var result = _sessionService.UpdateSession(id, updatedSession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Session";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Session Delete

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionById(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = session.Id;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _sessionService.RemoveSession(id);
            if (result) { 
                TempData["SuccessMessage"] = "Session Deleted";
            }
            else
            {
                TempData["ErrorMessage"] = "Session Cannot be Deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Helper Methods

        private void LoadDropDownsForTrainers()
        {
            var Trainers = _sessionService.GetTrainerForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }
        private void LoadDropDownsForCategories()
        {
            var Categories = _sessionService.GetCategoryForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        }

        #endregion
    }
}
