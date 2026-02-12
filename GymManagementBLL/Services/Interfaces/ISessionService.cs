using GymManagementBLL.ViewModels.SessionViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSession();
        SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionViewModel CreatedSession);

    }
}
