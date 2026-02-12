using AutoMapper;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            #region Session => SessionViewModel

            CreateMap<Session, SessionViewModel>()
                    .ForMember(des => des.CatedoryName, options =>
                    {
                        options.MapFrom(src => src.SessionCategory.CategoryName);
                    })
                    .ForMember(des => des.TrainerName, options =>
                    {
                        options.MapFrom(src => src.SessionTrainer.Name);
                    })
                    .ForMember(des=>des.AvailableSlots , options=> options.Ignore());

            #endregion

            #region SessionViewModel => Session

            CreateMap<SessionViewModel, Session>();
                
                

            #endregion
        }
    }
}
