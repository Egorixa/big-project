using Microsoft.EntityFrameworkCore;
using StudyService.Models;

namespace StudyService.Data
{
    public class StudyServiceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Response> Responses { get; set; }

        public StudyServiceDbContext(DbContextOptions<StudyServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.Name).HasColumnName("name").IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Surname).HasColumnName("surname").IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email).HasColumnName("email").IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Password).HasColumnName("password").IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Stage).HasColumnName("stage").HasDefaultValue(0);
            modelBuilder.Entity<User>()
                .Property(u => u.Course).HasColumnName("course").IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.ProfileDescription).HasColumnName("profiledescription");
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            // Ads
            modelBuilder.Entity<Ad>()
                .ToTable("ads")
                .HasKey(a => a.Id);
            modelBuilder.Entity<Ad>()
                .Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Ad>()
                .Property(a => a.Title).HasColumnName("title").IsRequired();
            modelBuilder.Entity<Ad>()
                .Property(a => a.Description).HasColumnName("description");
            modelBuilder.Entity<Ad>()
                .Property(a => a.Type).HasColumnName("type").IsRequired();
            modelBuilder.Entity<Ad>()
                .Property(a => a.UserId).HasColumnName("userid").IsRequired();
            modelBuilder.Entity<Ad>()
                .Property(a => a.Price).HasColumnName("price");
            modelBuilder.Entity<Ad>()
                .Property(a => a.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Responses
            modelBuilder.Entity<Response>()
                .ToTable("responses")
                .HasKey(r => r.Id);
            modelBuilder.Entity<Response>()
                .Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Response>()
                .Property(r => r.AdId).HasColumnName("adid").IsRequired();
            modelBuilder.Entity<Response>()
                .Property(r => r.UserId).HasColumnName("userid").IsRequired();
            modelBuilder.Entity<Response>()
                .Property(r => r.Message).HasColumnName("message").IsRequired();
            modelBuilder.Entity<Response>()
                .Property(r => r.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}