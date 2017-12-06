using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;


namespace GigHub.Persistence.Entity_Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public UserConfiguration()
        {
            HasMany(u => u.Followers).WithRequired(f => f.Followee).WillCascadeOnDelete(false);
            HasMany(f => f.Followees).WithRequired(u => u.Follower).WillCascadeOnDelete(false);

            Property(u => u.Name).IsRequired().HasMaxLength(100);

        }
    }
}