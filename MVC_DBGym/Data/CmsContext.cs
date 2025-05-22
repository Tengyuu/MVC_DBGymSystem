using Microsoft.EntityFrameworkCore;
using MVC_DBGym.Models;

namespace MVC_DBGym.Data
{
    public class CmsContext:DbContext
    {
        public CmsContext(DbContextOptions<CmsContext> options) : base(options)
        {

        }
        public DbSet<DBCourses>  Coaches{ get; set; }
        public DbSet<DBCourses>  Courses{ get; set; }
        public DbSet<DBCourses>  Members{ get; set; }
        public DbSet<DBCourses>  Payment{ get; set; }
        public DbSet<DBCourses>  PaymentType{ get; set; }
        public DbSet<DBCourses>  PType{ get; set; }
        public DbSet<DBCourses>  Reserve{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
