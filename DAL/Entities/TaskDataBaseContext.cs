
using Microsoft.EntityFrameworkCore;

namespace Task.DAl.Entities
{
    public partial class TaskDataBaseContext : DbContext
    {
        public TaskDataBaseContext()
        {
        }

        public TaskDataBaseContext(DbContextOptions<TaskDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Governate> Governates { get; set; } = null!;
        public virtual DbSet<Village> Villages { get; set; } = null!;
        public virtual DbSet<EmployeeReportV> EmployeeReportV { get; set; } = null!;

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeReportV>(entity =>
            {
                entity.HasNoKey();

            });
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameAr)
                    .HasMaxLength(50)
                    .HasColumnName("Name_AR");

                entity.HasOne(d => d.Governate)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.GovernateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cities__Governat__267ABA7A");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.NationalId).HasMaxLength(14);

                entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Telephone).HasMaxLength(11);

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.VillageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Villag__2C3393D0");
            });

            modelBuilder.Entity<Governate>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameAr)
                    .HasMaxLength(50)
                    .HasColumnName("Name_AR");
            });

            modelBuilder.Entity<Village>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameAr)
                    .HasMaxLength(50)
                    .HasColumnName("Name_AR");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Villages)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Villages__CityId__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
