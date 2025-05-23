using Microsoft.EntityFrameworkCore;
using MVC_DBGym.Models;
using System.Data;

namespace MVC_DBGym.Data
{
    public class CmsContext:DbContext
    {
        public CmsContext(DbContextOptions<CmsContext> options) : base(options)
        {

        }
        public DbSet<DBCoaches>  Coaches{ get; set; }
        public DbSet<DBCourses>  Courses{ get; set; }
        public DbSet<DBPayment>  Payments{ get; set; }
        public DbSet<DBPaymentType>  PaymentTypes{ get; set; }
        public DbSet<DBPtype>  PTyps{ get; set; }
        public DbSet<DBReserve>  Reserves{ get; set; }
        public DbSet<DBMembers> Members { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBCoaches>().HasKey(c => c.CoachID);
            modelBuilder.Entity<DBCourses>().HasKey(c => c.CourseID);
            modelBuilder.Entity<DBMembers>().HasKey(c => c.MemberID);
            modelBuilder.Entity<DBPayment>().HasKey(c => c.PaymentID);
            modelBuilder.Entity<DBPtype>().HasKey(c => c.PTypeID);
    
            // 複合主鍵：MemberID + CourseID
            modelBuilder.Entity<DBReserve>()
                .HasKey(r => new { r.MemberID, r.CourseID });
            modelBuilder.Entity<DBPaymentType>()
                .HasKey(r => new { r.PaymentID, r.PTypeID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
