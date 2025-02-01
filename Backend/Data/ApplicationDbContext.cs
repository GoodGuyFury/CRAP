using code_review_analysis_platform.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace code_review_analysis_platform.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Credentials>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Credentials>(c => c.UserId)
                .HasPrincipalKey<User>(u => u.UserId);

        }
    }
}
