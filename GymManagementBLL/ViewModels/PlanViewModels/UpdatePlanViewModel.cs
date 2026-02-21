
using System.ComponentModel.DataAnnotations;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
       
        public string PlanName { get; set; } = null!;

        [Required(ErrorMessage = "Description Is Required")]
        [StringLength(200,MinimumLength = 5, ErrorMessage = "Plan Name Must Be Less Than 200 Chars and Greater Than 5")]
        public string Description { get; set; }= null!;

        [Required(ErrorMessage = "Duration Days Is Required")]
        [Range(1,365,ErrorMessage ="Duration Days Must Be Between 1 and 365 ")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price Days Is Required")]
        [Range(0.1, 10000, ErrorMessage = "Price Days Must Be Between 0.1 and 10000 ")]
        public decimal Price { get; set; }
    }
}
