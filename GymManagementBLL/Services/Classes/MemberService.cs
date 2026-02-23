using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace GymManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //CLR Will inject address of object in constructor
        // Constructor: Inject required repositories via Dependency Injection
        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Get all members and map them to MemberViewModel.
        // Returns an empty collection if no members exist.
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if(members is null || !members.Any()) return [];

           

            #region Manual Mapping 02 [ Projection ]

            var memberViewModels = _mapper.Map<IEnumerable<MemberViewModel>>(members);

            #endregion


            return memberViewModels;
        }

        // Creates a new member with associated address and health record.
        // Returns false if email or phone already exists.
        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                // if one of this return false 
                if (IsEmailExists(createMember.Email) || IsPhoneExists(createMember.Phone)) return false;

                // if not Add member and return true
                var member = _mapper.Map<CreateMemberViewModel,Member>(createMember);

                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }

        // Get detailed information about a member including active membership and plan.
        // Returns null if member not found.
        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

            if (member is null)
                return null;

            var viewModel = _mapper.Map<MemberViewModel>(member);

            //get Active MemberShip
            var ActiveMemberShip = _unitOfWork.GetRepository<MemberShip>().GetAll(x => x.MemberId == memberId && x.Status == "Active").FirstOrDefault();
            if(ActiveMemberShip is not null)
            {

                viewModel.MemberShipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                viewModel.MemberShipStartDate = ActiveMemberShip.EndDate.ToShortDateString();

                var Plan = _unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                viewModel.PlanName = Plan?.Name;
            }
            return viewModel;
        }

        // Get health record details for a specific member.
        // Returns null if not found.
        public HealthRecordViewModel? GetMemberHealthRecordDetails(int memberId)
        {
            var memberHealthReacord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);
            
            if(memberHealthReacord is null) return null;

            var HealthRecordViewModel = _mapper.Map<HealthRecordViewModel>(memberHealthReacord);

            return HealthRecordViewModel;
        }

        // Get member information to populate update form.
        // Returns null if member not found.
        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            
            if(member is null) return null;

            return _mapper.Map<MemberToUpdateViewModel>(member);
        }

        // Update member details including address and contact information.
        // Returns false if member not found or email/phone already exists.
        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                var EmailExist = _unitOfWork.GetRepository<Member>()
                                .GetAll(X => X.Email == UpdatedMember.Email && X.Id != Id);
                var PhoneExist = _unitOfWork.GetRepository<Member>()
                                .GetAll(X => X.Phone == UpdatedMember.Phone && X.Id != Id);

                if(EmailExist.Any() || PhoneExist.Any()) 
                    return false;

                var member = _unitOfWork.GetRepository<Member>().GetById(Id);
                if (member is null) return false;

                _mapper.Map(UpdatedMember, member);

                _unitOfWork.GetRepository<Member>().Update(member) ;
                return _unitOfWork.SaveChanges() > 0;
                
            }
            catch 
            {
                return false;
            }
        }

        public bool RemoveMember(int MemberId)
        {
            var MemberRepo = _unitOfWork.GetRepository<Member>();
            var MemberShipRepo = _unitOfWork.GetRepository<MemberShip>();
            var MemberSession = _unitOfWork.GetRepository<MemberSession>();

            var member = MemberRepo.GetById(MemberId);
            if (member is null) return false;

            var SessionIds = MemberSession.GetAll(X=>X.MemberId == MemberId ).Select(X=>X.SessionId);
            var HasFutureSessions = _unitOfWork.GetRepository<Session>().GetAll(
                X=> SessionIds.Contains(X.Id) && X.StartDate > DateTime.Now
            ).Any();
            if(HasFutureSessions) return false;

            var memberShips = MemberShipRepo.GetAll(Q => Q.MemberId == MemberId);
            try
            {
                if (memberShips.Any())
                {
                    foreach(var memberShip in memberShips)
                    {
                        MemberShipRepo.Delete(memberShip);
                    }
                }
                MemberRepo.Delete(member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        #region Helpers Methods

        // Check if email already exists in the system.
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(Q=>Q.Email == email).Any();
        }

        // Check if phone already exists in the system.
        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(Q => Q.Phone == phone).Any();
        }

        

        #endregion
    }
}
