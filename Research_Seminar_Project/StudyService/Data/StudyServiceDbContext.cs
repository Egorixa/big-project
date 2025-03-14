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
            // Настройки для Ad
            modelBuilder.Entity<Ad>()
                .ToTable("ads")
                .Property(a => a.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Ad>()
                .Property(a => a.Title)
                .HasColumnName("title");
            modelBuilder.Entity<Ad>()
                .Property(a => a.Description)
                .HasColumnName("description");
            modelBuilder.Entity<Ad>()
                .Property(a => a.Type)
                .HasColumnName("type")
                .HasConversion<int>();
            modelBuilder.Entity<Ad>()
                .Property(a => a.UserId)
                .HasColumnName("userid");
            modelBuilder.Entity<Ad>()
                .Property(a => a.Price)
                .HasColumnName("price");
            modelBuilder.Entity<Ad>()
                .Property(a => a.CreatedAt)
                .HasColumnName("createdat");

            // Настройки для User
            modelBuilder.Entity<User>()
                .ToTable("users")
                .Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasColumnName("name");
            modelBuilder.Entity<User>()
                .Property(u => u.Surname)
                .HasColumnName("surname");
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnName("email");
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasColumnName("password");
            modelBuilder.Entity<User>()
                .Property(u => u.Stage)
                .HasColumnName("stage");
            modelBuilder.Entity<User>()
                .Property(u => u.Course)
                .HasColumnName("course");
            modelBuilder.Entity<User>()
                .Property(u => u.ProfileDescription)
                .HasColumnName("profiledescription");

            // Настройки для Response
            modelBuilder.Entity<Response>()
                .ToTable("responses")
                .Property(r => r.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Response>()
                .Property(r => r.AdId)
                .HasColumnName("adid");
            modelBuilder.Entity<Response>()
                .Property(r => r.UserId)
                .HasColumnName("userid");
            modelBuilder.Entity<Response>()
                .Property(r => r.Message)
                .HasColumnName("message");
            modelBuilder.Entity<Response>()
                .Property(r => r.CreatedAt)
                .HasColumnName("createdat");
        }
    }
}