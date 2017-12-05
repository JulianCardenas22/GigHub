using GigHub.Core.Models;
using GigHub.Persistence.Entity_Configuration;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances{ get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new GigConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new AttendanceConfiguration());



            modelBuilder.Entity<UserNotification>().HasRequired(n => n.User)
                                                   .WithMany(u => u.UserNotifications)
                                                   .WillCascadeOnDelete(false);



            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}

