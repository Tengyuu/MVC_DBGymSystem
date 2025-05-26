using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC_DBGym.Models
{
    public class DBCourses
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime CourseDate { get; set; }
        public int CoachID { get; set; }
        [ValidateNever]
        public DBCoaches Coach { get; set; }
        [ValidateNever]
        public ICollection<DBReserve> Reserves { get; set; }

    }
}
