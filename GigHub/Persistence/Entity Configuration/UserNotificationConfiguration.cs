using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;


namespace GigHub.Persistence.Entity_Configuration
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(a => new { a.UserId, a.NotificationId });
        }
    }
}