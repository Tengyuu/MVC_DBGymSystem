using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC_DBGym.Models
{
    public class Coach
    {
        public int CoachID { get; set; }
        public string CoachName { get; set; }
        public string Gender {  get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        [ValidateNever]
        public ICollection<Course> Course { get; set; } = new List<Course>();

    }
}
