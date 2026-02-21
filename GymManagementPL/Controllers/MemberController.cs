using GymManagementBLL.Services.Interfaces;
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
                return RedirectToAction(nameof(Index));
        
            var member = _memberService.GetMemberDetails(id);

            if(member == null)
                return RedirectToAction(nameof(Index));

            return View(member);
        }

        #endregion
    }
}
