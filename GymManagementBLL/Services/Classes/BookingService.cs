using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory()
        {
            var SessionRepo = _unitOfWork.SessionRepository;
            var sessions = SessionRepo.GetAllSessionsWithTrainerAndCategory();

            var sessionViewModels = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);

            foreach(var session in sessionViewModels)
            {
                session.AvailableSlots = session.Capacity - SessionRepo.GetCountOfBookSlots(session.Id);
            }

            return sessionViewModels;
        }
        public IEnumerable<MemberForSessionViewModel> GetAllMembersForUpcomingSession(int id)
        {
            var repo = _unitOfWork.BookingRepository;
            var memberOfSessions = repo.GetSessionById(id);
            var memberForSessionViewModel = _mapper.Map<IEnumerable<MemberForSessionViewModel>>(memberOfSessions);
            return memberForSessionViewModel;
        }
    

    }
}
