using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;
using GigHub.Repositories;


namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs  { get;private set; }
        public IGenreRepository Genres { get;private set; }
        public IFollowRepository Follows { get;private set; }
        public IAttendanceRepository Attendances { get;private set; }


        public IApplicationUserRepository Users { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }



        public UnitOfWork(ApplicationDbContext context){
            _context = context;
            Gigs = new GigRepository(_context);
            Attendances = new AttendanceRepository(context);
            Genres = new GenreRepository(context);
            Follows = new FollowRepository(context);
            Users = new ApplicationUserRepository(context);
            Notifications = new NotificationRepository(context);
            UserNotifications = new UserNotificationRepository(context);
        }

        public void Complete(){
            _context.SaveChanges();
        }
    }
}