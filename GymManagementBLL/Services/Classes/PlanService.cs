

using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();

            if (Plans is null || !Plans.Any()) return [];

            return Plans.Select(X => new PlanViewModel()
            {
                Id = X.Id,
                Name = X.Name,
                Description = X.Description,
                DurationDays = X.DurationDays,
                IsActive = X.IsActive,
                Price = X.Price
            });
        }

        public PlanViewModel? GetPlanById(int planId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if(Plan is null) return null;

            return new PlanViewModel()
            {
                Id = Plan.Id,
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                IsActive = Plan.IsActive,
                Price = Plan.Price
                
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int planId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if(Plan is null || Plan.IsActive == false || HasActiveMemberShips(planId)) return null;

            return new UpdatePlanViewModel()
            {
                Description = Plan.Description,
                DurationDays= Plan.DurationDays,
                PlanName = Plan.Name,
                Price= Plan.Price
            };
        }
        
        public bool UpdatePlan(int planId, UpdatePlanViewModel UpdatedPlan)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (Plan is null || HasActiveMemberShips(planId)) return false;

            try
            {
                (Plan.Description, Plan.Price, Plan.DurationDays, Plan.UpdatedAt)
                = (UpdatedPlan.Description, UpdatedPlan.Price, UpdatedPlan.DurationDays, DateTime.Now);

                _unitOfWork.GetRepository<Plan>().Update(Plan);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        // SoftDelete
        public bool ToggleStatus(int planId)
        {
            var repo = _unitOfWork.GetRepository<Plan>();

            var Plan = repo.GetById(planId);
            if (Plan is null || HasActiveMemberShips(planId)) return false;

            Plan.IsActive = Plan.IsActive == true ? false : true;
            Plan.UpdatedAt = DateTime.Now;
            try
            {
                repo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }


        }

        
        #region Helper Methods

        private bool HasActiveMemberShips(int PlanId)
        {
            var ActiveMemberShips = _unitOfWork.GetRepository<MemberShip>()
                                               .GetAll(x=>x.PlanId == PlanId && x.Status == "Active");
            
            return ActiveMemberShips.Any();
        }

        #endregion
    }
}
