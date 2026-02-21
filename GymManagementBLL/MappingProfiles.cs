using AutoMapper;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            MapSession();
            MapMember();
            MapTrainer();
            MapPlan();

        }

        private void MapSession()
        {
            #region Session => SessionViewModel

            CreateMap<Session, SessionViewModel>()
                    .ForMember(des => des.CatedoryName, options =>{options.MapFrom(src => src.SessionCategory.CategoryName);})
                    .ForMember(des => des.TrainerName, options =>{options.MapFrom(src => src.SessionTrainer.Name);})
                    .ForMember(des => des.AvailableSlots, options => options.Ignore());

            #endregion

            #region SessionViewModel => Session

            CreateMap<SessionViewModel, Session>();

            #endregion

            #region Session => UpdatedSessionViewModel and Reverse

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();

            #endregion
        }
        
        private void MapMember()
        {

            #region createMemberViewModel => Member

            CreateMap<CreateMemberViewModel, Member>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                    .ForMember(dest => dest.HealthRecord,opt => opt.MapFrom(src => src.HealthRecordViewModel));

            CreateMap<CreateMemberViewModel, Address>()
                .ForMember(des => des.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
                .ForMember(des => des.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(des => des.City, opt => opt.MapFrom(src => src.City));

            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

            #endregion

            #region Member => memberViewModel

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

            #endregion

            #region Member => MemberToUpdateViewModel

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            #endregion

            #region  MemberToUpdateViewModel => Member

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src , dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });


            #endregion
        }

        private void MapTrainer()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));
            CreateMap<Trainer, TrainerViewModel>();
            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dist => dist.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

            CreateMap<TrainerToUpdateViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.UpdatedAt = DateTime.Now;
            });
        }

        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();
            CreateMap<Plan, UpdatePlanViewModel>().ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Name));
            CreateMap<UpdatePlanViewModel, Plan>()
           .ForMember(dest => dest.Name, opt => opt.Ignore())
           .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
