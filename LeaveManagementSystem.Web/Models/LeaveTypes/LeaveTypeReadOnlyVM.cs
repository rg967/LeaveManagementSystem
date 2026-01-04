using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
    }
}
