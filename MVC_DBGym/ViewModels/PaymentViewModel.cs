using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC_DBGym.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        public int MemberID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "金額需大於 0")]
        public int Amount { get; set; }

        [Required]
        public int PTypeID { get; set; }    
        public List<SelectListItem> PTypeList { get; set; } = new List<SelectListItem>();
    }
}
