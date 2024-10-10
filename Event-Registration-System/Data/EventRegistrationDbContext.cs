using Event_Registration_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Registration_System.Data
{
    public class EventRegistrationDbContext : DbContext
    {
        public EventRegistrationDbContext(DbContextOptions<EventRegistrationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}
