using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MembershipViewModels;
using GymManagementDAL.Entities;
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
                                        .GetAllMembershipsWithMembersAndPlans(m=>m.Status == "Active");
            var membershipsViewModel = _mapper.Map<IEnumerable<MembershipViewModel>>(memberShips);
            return membershipsViewModel;
        }

        public bool CreateMembership(CreateMembershipViewModel model)
        {
            // Validate:
            // 1. Member must exist
            // 2. Plan must exist
            // 3. Member must NOT already have an active membership
            if (!IsMemberExist(model.MemberId) || !IsPlanExist(model.PlanId) || HasActiveMemberships(model.MemberId) ) 
                return false;

            var membershipRepo = _unitOfWork.MembershipRepository; //repo
            var membershipToCreate = _mapper.Map<MemberShip>(model);

            var plan = _unitOfWork.GetRepository<Plan>().GetById(model.PlanId);

            // Set membership end date based on plan duration
            membershipToCreate.EndDate = DateTime.UtcNow.AddDays(plan!.DurationDays);

            

            membershipRepo.Add(membershipToCreate);
            
            return _unitOfWork.SaveChanges() > 0 ;
        }

        public bool DeleteMembership(int MemberId)
        {
            var membershipRepo = _unitOfWork.MembershipRepository;
            var membershipForDelete = membershipRepo.GetFirstOrDefault(m => m.MemberId == MemberId && m.Status == "Active");
            if(membershipForDelete is null )return false;

            membershipRepo.Delete(membershipForDelete);
            return _unitOfWork.SaveChanges() > 0;
        }


        #region Helper Methods

        // Checks member exists in the database by their ID.
        private bool IsMemberExist(int memberId)
            => _unitOfWork.GetRepository<Member>().GetById(memberId) is not null;

        // Checksplan exists in the database by its ID.
        private bool IsPlanExist(int PlanId)
            => _unitOfWork.GetRepository<Plan>().GetById(PlanId) is not null;

        // Determines whether a member has any active memberships.
        private bool HasActiveMemberships(int memberId)
        => _unitOfWork.MembershipRepository.GetAllMembershipsWithMembersAndPlans(m => m.Status.ToLower() == "active" && m.MemberId == memberId).Any();

        public IEnumerable<PlanForSelectListViewModel> GetPlansForDropDown()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll(plan => plan.IsActive);
            var plansSelectList = _mapper.Map<IEnumerable<PlanForSelectListViewModel>>(plans);
            return plansSelectList;
        }

        public IEnumerable<MemberForSelectListViewModel> GetMemberForDropDown()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            var membersSelectList = _mapper.Map<IEnumerable<MemberForSelectListViewModel>>(members);
            return membersSelectList;
        }

       
        #endregion
    }
}
