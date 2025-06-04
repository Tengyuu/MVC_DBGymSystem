using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Xml;

namespace MVC_DBGym.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime CourseDate { get; set; }
        public int CoachID { get; set; }
        [ValidateNever]
        public Coach Coach { get; set; }
        [ValidateNever]
        public ICollection<Reserve> Reserve { get; set; } = new List<Reserve>();

    }
}
