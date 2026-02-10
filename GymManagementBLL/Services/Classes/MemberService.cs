using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;
        //CLR Will inject address of object in constructor
        public MemberService(IGenericRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
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
        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                // check if phone is exists
                var phoneExist = _memberRepository.GetAll(x => x.Phone == createMember.Phone).Any();

                // check if email is exists
                var emailExist = _memberRepository.GetAll(x => x.Email == createMember.Email).Any();

                // if one of this return false 
                if (phoneExist || emailExist) return false;

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

                return _memberRepository.Add(member) > 0;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}
