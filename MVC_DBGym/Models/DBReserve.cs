namespace MVC_DBGym.Models
{
    public class DBReserve
    {
        public int MemberID { get; set; }
        public DBMembers Members { get; set; }

        public int CourseID { get; set; }
        public DBCourses Courses { get; set; }


    }
}
