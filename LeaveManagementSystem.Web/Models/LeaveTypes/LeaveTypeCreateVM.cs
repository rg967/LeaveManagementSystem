using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeCreateVM
    {
        [Required]
        [Length(4,150, ErrorMessage ="Doesn't meet length criteria")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1,90)]
        [Display(Name="Number of Days")]
        public int NumberOfDays { get; set; }
    }
}
