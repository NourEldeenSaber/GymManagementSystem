

using GymManagementBLL.ViewModels.SessionViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSession();
    }
}
