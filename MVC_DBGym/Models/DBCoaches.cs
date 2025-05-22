namespace MVC_DBGym.Models
{
    public class DBCoaches
    {
        public int CoachID { get; set; }
        public string CoachName { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }

        public ICollection<DBCourses> Courses { get; set; }

    }
}
