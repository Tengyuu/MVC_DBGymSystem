using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC_DBGym.Models
{
    public class DBMembers
    {
        public int MemberID { get; set; }
        [Display(Name = "電話")]
        [Required(ErrorMessage = "電話不可為空")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "姓名不可為空")]
        [Display(Name ="姓名")]
        public string MemberName { get; set; }

        [Required(ErrorMessage = "密碼不可為空")]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "性別")]
        public string Gender { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "電話不可為空")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.Now;

        public string Role { get; set; } = "Member";

        [ValidateNever]
        public ICollection<DBPayment> Payments { get; set; }
        [ValidateNever]
        public ICollection<DBReserve> Reserves { get; set; }
    }
}
