using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        #region GetAllMembers

        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();

            return View(members);
        }

        #endregion

        #region GetMemberData

        public IActionResult MemberDetails(int id) 
        { 
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member cannot be 0 or Negative number";
                return RedirectToAction(nameof(Index));
            }
        
            var member = _memberService.GetMemberDetails(id);

            if(member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        public IActionResult HealthRecordDetails(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member cannot be 0 or Negative number";
                return RedirectToAction(nameof(Index));
            }

            var HealthRecord = _memberService.GetMemberHealthRecordDetails(id);
            
            if(HealthRecord is  null)
            {
                TempData["ErrorMessage"] = "HealthRecord Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(HealthRecord);

        }

        #endregion

        #region Create Member

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel CreatedMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Field");
                return View(nameof(Create),CreatedMember);
            }

            bool Result = _memberService.CreateMember(CreatedMember);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed to Create, check phone and email ";

            }
            return RedirectToAction(nameof(Index));

        }

        #endregion

        #region Edit Member

        public IActionResult MemberEdit(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member cannot be 0 or Negative number";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberToUpdate(id);
            if (member is null) 
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public IActionResult MemberEdit([FromRoute]int id , MemberToUpdateViewModel memberUpdated )
        {
            if (!ModelState.IsValid)
               return View(memberUpdated);
            
            var Result = _memberService.UpdateMemberDetails(id, memberUpdated);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete Member

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member cannot be 0 or Negative number";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberToUpdate(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = member.Name;
            return View();
        }

        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm]int id)
        {
            var Result = _memberService.RemoveMember(id);
            if (Result)
                TempData["SuccessMessage"] = "Member Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Member cannot Deleted ";
            
            return RedirectToAction(nameof(Index));
        }

        #endregion
    
    }
}
