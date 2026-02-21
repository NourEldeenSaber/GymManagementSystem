using GymManagementDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    public class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Name Is Requierd")]
        [StringLength(50 , MinimumLength = 2, ErrorMessage = "Name Must Be Between 2 And 50 Char")]
        [RegularExpression(@"^[a-zA-Z\s]+$",ErrorMessage ="Name Can Contain Only Letters And Spaces")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email Is Requierd")]
        [EmailAddress(ErrorMessage ="Invalid Email Format")] //Validation Format
        [DataType(DataType.EmailAddress)] // Ui Hint
        [StringLength(maximumLength:100,MinimumLength =5,ErrorMessage ="Email Must Be Between 5 and 100 char ")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Is Requierd")]
        [Phone(ErrorMessage ="Invalid Phone Format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$",ErrorMessage ="Phone Number Must Be Egyptian Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "DateOfBirth Is Requierd")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "DateOfBirth Is Requierd")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "BuildingNumber Is Requierd")]
        [Range(1,999,ErrorMessage ="Building Number Must Be Between 1 and 999")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street Is Requierd")]
        [StringLength(maximumLength:30, MinimumLength =2, ErrorMessage = "Street Must Be Between 2 and 30 Chars")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City Is Requierd")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 and 30 Chars")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Contain Only Letters And Spaces")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Health Record Is Requierd")]
        public HealthRecordViewModel HealthRecordViewModel { get; set; } = null!;

    }
}
