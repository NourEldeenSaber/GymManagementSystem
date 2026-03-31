using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        #region Get All Plan 
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }
        #endregion

        #region Plan Details

        public IActionResult Details(int id) 
        { 
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanById(id);
            if(plan is null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        #endregion

        #region Edit Plan

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanToUpdate(id);
            if (plan is null) 
            {
                TempData["ErrorMessage"] = "Plan Cannot Be Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id , UpdatePlanViewModel updatedPlan )
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data Validation");
                return View(updatedPlan);
            }
            var result = _planService.UpdatePlan(id, updatedPlan);
            if (result)
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully!!";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed to Update";
            }
            return RedirectToAction(nameof(Index));

        }

        #endregion

        #region Activate
        [HttpPost]
        public IActionResult Activate([FromRoute]int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var result = _planService.ToggleStatus(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Plan Status Changed ";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Chanfe Plan Status";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
