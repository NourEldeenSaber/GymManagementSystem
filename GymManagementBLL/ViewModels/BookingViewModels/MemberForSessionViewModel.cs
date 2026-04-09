namespace GymManagementBLL.ViewModels.BookingViewModels
{
    public class MemberForSessionViewModel
    {
        public  int MemberId { get; set; }
        public string MemberName { get; set; }
        public string BookingDate { get; set; }
        public bool IsAttended { get; set; }
        public int SessionId { get; set; }
    }
}
