using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP1.API.Data.Models;

namespace TP1.API.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ville>()
                .Property(v => v.Region)
                .HasConversion<string>();
            
            modelBuilder.Entity<Evenement>()
                .HasMany(e => e.Participations)
                .WithOne(p => p.Evenement)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participation>().Property<bool>("EstValide");
            modelBuilder.Entity<Participation>()
                .HasQueryFilter(p => EF.Property<bool>(p, "EstValide") == true);
        }

        public override int SaveChanges()
        {
            VerifierValiditeParticipation();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            VerifierValiditeParticipation();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void VerifierValiditeParticipation()
        {
            foreach (var entree in ChangeTracker.Entries())
            {
                var estValideProp = entree.Properties.FirstOrDefault(p => p.Metadata.Name == "EstValide");
                if (estValideProp is null)
                    continue;

                estValideProp.CurrentValue = entree.State switch
                {
                    EntityState.Added => false,
                    EntityState.Modified => true,
                    _ => estValideProp.CurrentValue
                };
            }
        }
    }
}