using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
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
        public IEnumerable<MemberForSessionViewModel> GetMembersSession(int id)
        {
            var repo = _unitOfWork.BookingRepository;

            var memberOfSessions = repo.GetSessionById(id);
            var memberForSessionViewModel = _mapper.Map<IEnumerable<MemberForSessionViewModel>>(memberOfSessions);
            return memberForSessionViewModel;
        }
        public bool ToggleIsAttend(int memberId , int SessionId)
        {
            var repo = _unitOfWork.BookingRepository;
            var booking = repo.GetSessionByMemberIdAndSessionId(memberId, SessionId);
            
            if (booking == null) return false;

            booking.IsAttended = booking.IsAttended == true ? false : true;
            booking.UpdatedAt = DateTime.UtcNow;
            try
            {
                repo.Update(booking);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
            
        }

        public bool CreateBooking(CreateBookingViewModel model)
        {
            var session = _unitOfWork.SessionRepository.GetById(model.SessionId); 
            if (session == null || session.StartDate <= DateTime.UtcNow) return false;

            var membershipRepo = _unitOfWork.MembershipRepository;

            var activeMembershipForMember = membershipRepo.GetFirstOrDefault(m => m.Status == "Active" && m.MemberId == model.SessionId);
            if (activeMembershipForMember is null) return false;

            var sessionRepo = _unitOfWork.SessionRepository;
            
            var bookedSlots = sessionRepo.GetCountOfBookSlots(model.SessionId);
            var avilableSlots = session.Capacity - bookedSlots;
            if (avilableSlots == 0) return false;

            var booking = _mapper.Map<MemberSession>(model);
            booking.IsAttended = false; 

            _unitOfWork.BookingRepository.Add(booking);
            return _unitOfWork.SaveChanges() > 0;

        }
    }
}
