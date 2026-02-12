using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<SessionViewModel> GetAllSession()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (Sessions is null || !Sessions.Any()) return [];

            var MappedSessions = _mapper.Map<IEnumerable<Session> , IEnumerable<SessionViewModel>>(Sessions);

            foreach (var session in MappedSessions)
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id);

            return MappedSessions;
        }
        public SessionViewModel? GetSessionById(int id)
        {
            var Session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(id);
            if(Session is null) return null;
            
            var mappedSession =_mapper.Map<Session,SessionViewModel>(Session);
            mappedSession.AvailableSlots = mappedSession.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(mappedSession.Id);

            return mappedSession;
        }
        public bool CreateSession(CreateSessionViewModel CreatedSession)
        {
            try
            {
                // check if Trainer Exsists
                if (!IsTrainerExsists(CreatedSession.TrainerId)) return false;

                // check if Category Exists
                if (!IsCategoryExsists(CreatedSession.CategoryId)) return false;

                //Check if EndDate After StartDate
                if (!IsDateTimeValid(CreatedSession.StartDate, CreatedSession.EndDate)) return false;

                if (CreatedSession.Capacity > 25 || CreatedSession.Capacity < 0) return false;

                var sessionEntity = _mapper.Map<Session>(CreatedSession);

                _unitOfWork.GetRepository<Session>().Add(sessionEntity);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Create Session Faild : {ex}");
                return false;
            }

        }



        #region Helpers Methods

        private bool IsTrainerExsists(int TrainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(TrainerId) is not null;
        }
        private bool IsCategoryExsists(int CategoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(CategoryId) is not null;
        }
        private bool IsDateTimeValid(DateTime startDate , DateTime EndDate)
        {
            return startDate < EndDate;
        }

        #endregion
    }
}
