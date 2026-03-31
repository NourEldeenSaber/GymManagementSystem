using System.ComponentModel.DataAnnotations;

namespace GymManagementBLL.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email Is Requierd")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password Is Requierd")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; } 
    }
}
