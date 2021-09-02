using System.Data.Entity;
using System.Linq;

namespace ApplicationDevelopment.Entities
{
    public partial class TrainingProgramManagerDbContext : DbContext
    {
        public TrainingProgramManagerDbContext()
            : base("name=TrainingProgramManagerDbContext")
        {
            Database.SetInitializer(new DataSeeding());
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AssignedCourse> AssignedCourses { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseCategory> CourseCategories { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.MainPpLanguage)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.isActive)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AssignedCourses)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Course>()
                .HasOptional(e => e.AssignedCourse)
                .WithRequired(e => e.Course);
        }        
    }
}
