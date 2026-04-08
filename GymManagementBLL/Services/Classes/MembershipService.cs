using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MembershipViewModels;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MembershipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<MembershipViewModel> GetAllMemberShips()
        {
            var memberShips = _unitOfWork.MembershipRepository
                                        .GetAllMembershipsWithMembersAndPlans(m=>m.Status.ToLower() == "active");
            var membershipsViewModel = _mapper.Map<IEnumerable<MembershipViewModel>>(memberShips);
            return membershipsViewModel;
        }
    }
}
