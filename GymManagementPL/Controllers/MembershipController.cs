using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MembershipViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public IActionResult Index()
        {
            var memberShips = _membershipService.GetAllMemberShips();
            return View(memberShips);
        }


        #region Create

        public IActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMembershipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _membershipService.CreateMembership(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Membership Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Membership Can't Created";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Membership Can't Created, check your data";

            LoadDropDowns();
            return View(model);
        }

        #endregion


        #region Helper Methods

        private void LoadDropDowns()
        {
            var members = _membershipService.GetMemberForDropDown();
            var plans = _membershipService.GetPlansForDropDown();
            ViewBag.Members = new SelectList(members,"Id","Name");
            ViewBag.Plans = new SelectList(plans,"Id","Name");
        }

        #endregion
    }
}
