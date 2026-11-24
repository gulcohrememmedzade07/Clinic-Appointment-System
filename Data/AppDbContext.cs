using Microsoft.EntityFrameworkCore;
using Clinic_Appointment_System.Models;

namespace Clinic_Appointment_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
    }
}