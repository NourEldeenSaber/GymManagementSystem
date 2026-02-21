
using GymManagementBLL.ViewModels.PlanViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int planId);

        UpdatePlanViewModel? GetPlanToUpdate(int planId);
        bool UpdatePlan(int  planId , UpdatePlanViewModel UpdatedPlan);

        bool ToggleStatus(int planId);
    }
}
