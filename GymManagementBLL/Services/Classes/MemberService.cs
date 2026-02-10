using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        //CLR Will inject address of object in constructor
        // Constructor: Inject required repositories via Dependency Injection
        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get all members and map them to MemberViewModel.
        // Returns an empty collection if no members exist.
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if(members is null || !members.Any()) return [];

            #region Manual Mapping 01

            //var memberViewModels = new List<MemberViewModel>();
            //foreach (var member in members)
            //{
            //    var memberViewModel = new MemberViewModel()
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Email = member.Email,
            //        Phone = member.Phone,
            //        Photo = member.Photo,
            //        Gender = member.Gender.ToString(),
            //    };
            //    memberViewModels.Add(memberViewModel);
            //} 

            #endregion

            #region Manual Mapping 02 [ Projection ]

            var memberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Photo = m.Photo,
                Gender = m.Gender.ToString()
            });

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
                var member = new Member
                {
                    Name = createMember.Name,
                    Email = createMember.Email,
                    Phone = createMember.Phone,
                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = createMember.BuildingNumber,
                        City = createMember.City,
                        Street = createMember.Street,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMember.HealthRecordViewModel.Height,
                        Weight = createMember.HealthRecordViewModel.Weight,
                        BloodType = createMember.HealthRecordViewModel.BloodType,
                        Note = createMember.HealthRecordViewModel.Note
                    }
                };

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

            var viewModel = new MemberViewModel
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",
                Photo = member.Photo,

            };

            //get Active MemberShip
            var ActiveMemberShip = _unitOfWork.GetRepository<MemberShip>().GetAll(x=>x.MemberId == memberId && x.Status =="Active").FirstOrDefault();
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

            var HealthRecordViewModel = new HealthRecordViewModel
            {
                Height = memberHealthReacord.Height,
                Weight = memberHealthReacord.Weight,
                BloodType = memberHealthReacord.BloodType,
                Note    = memberHealthReacord.Note,
            };

            return HealthRecordViewModel;
        }

        // Get member information to populate update form.
        // Returns null if member not found.
        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            
            if(member is null) return null;

            return new MemberToUpdateViewModel()
            {
                Email = member.Email,
                Name = member.Name,
                Phone = member.Phone,
                Photo = member.Photo,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City,
            };
        }

        // Update member details including address and contact information.
        // Returns false if member not found or email/phone already exists.
        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                if (IsEmailExists(UpdatedMember.Email) || IsPhoneExists(UpdatedMember.Phone))  return false;

                var member = _unitOfWork.GetRepository<Member>().GetById(Id);
                if (member is null) return false;
                member.Email = UpdatedMember.Email;
                member.Phone = UpdatedMember.Phone;
                member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                member.Address.Street = UpdatedMember.Street;
                member.Address.City = UpdatedMember.City;
                member.UpdatedAt = DateTime.Now;
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

            var HasActiveMemberSession = MemberSession.GetAll(X=>X.MemberId == MemberId && X.Session.StartDate > DateTime.Now).Any();
            if(HasActiveMemberSession) return false;

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
