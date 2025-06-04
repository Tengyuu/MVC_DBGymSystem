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
        public DbSet<Coach>  Coache { get; set; }
        public DbSet<Course>  Course{ get; set; }
        public DbSet<Payment>  Payment{ get; set; }
        public DbSet<PaymentType>  PaymentType{ get; set; }
        public DbSet<Ptype>  PTyp{ get; set; }
        public DbSet<Reserve>  Reserve{ get; set; }
        public DbSet<Member> Member { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coach>().HasKey(c => c.CoachID);
            modelBuilder.Entity<Course>().HasKey(c => c.CourseID);
            modelBuilder.Entity<Member>().HasKey(c => c.MemberID);
            modelBuilder.Entity<Payment>().HasKey(c => c.PaymentID);
            modelBuilder.Entity<Ptype>().HasKey(c => c.PTypeID);

            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasPrecision(18, 2);
            // 複合主鍵：MemberID + CourseID
            modelBuilder.Entity<Reserve>()
                .HasKey(r => new { r.MemberID, r.CourseID });
            modelBuilder.Entity<Reserve>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Reserve)
                .HasForeignKey(r => r.CourseID)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reserve>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Reserve)
                .HasForeignKey(r => r.MemberID)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PaymentType>()
                .HasKey(r => new { r.PaymentID, r.PTypeID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
