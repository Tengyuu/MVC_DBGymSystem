using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_DBGym.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int Amount { get; set; }
        public DateTime PayDate { get; set; }

        public int MemberID { get; set; }
        public Member Member { get; set; }

        public int PTypeID { get; set; }
        [ForeignKey("PTypeID")]
        public PType PType { get; set; }

        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }

    }

}
