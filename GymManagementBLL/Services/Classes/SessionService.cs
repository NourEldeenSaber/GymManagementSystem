

using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSession()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (Sessions is null || !Sessions.Any()) return [];

            return Sessions.Select(session => new SessionViewModel
            {
                Id = session.Id,
                Description = session.Description,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                Capacity = session.Capacity,
                TrainerName = session.SessionTrainer.Name,
                CatedoryName = session.SessionCategory.CategoryName,
                AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id),
                
            });

            
        }
    }
}
