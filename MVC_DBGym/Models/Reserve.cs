using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_DBGym.Models
{
    public class Reserve
    {

        public int MemberID { get; set; }
        public Member Member { get; set; }


        public int CourseID { get; set; }
        public Course Course { get; set; }

        public bool IsPaid { get; set; } = false;

    }
}
