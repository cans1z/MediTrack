using MediTrack.Types;
using Microsoft.EntityFrameworkCore;

namespace MediTrack;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Medication> Medications { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<IntakeRecord> IntakeRecords { get; set; }
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=meditrack.db");
        /*optionsBuilder.UseNpgsql($"Host=localhost;" +
                                 $"Port=5432;" +
                                 $"Database=kekserv;" +
                                 $"Username=postgres;" +
                                 $"Password=1234");*/
    }
}