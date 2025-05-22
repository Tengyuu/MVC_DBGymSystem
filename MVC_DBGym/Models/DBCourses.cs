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
        public DBCoaches Coach { get; set; }

        public ICollection<DBReserve> Reserves { get; set; }

    }
}
