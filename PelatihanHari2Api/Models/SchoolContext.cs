using Microsoft.EntityFrameworkCore;

namespace PelatihanHari2Api.Models;

public class SchoolContext : DbContext
{
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Course> Course { get; set; }
    public virtual DbSet<Enrollment> Enrollment { get; set; }
    
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
        SavingChanges += SavingChangeEvent;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define a sequence
        // modelBuilder.HasSequence<int>("StudentSeq", schema: "fauzansaef")
        //     .StartsAt(1)
        //     .IncrementsBy(1);
        //
        // modelBuilder.HasSequence<int>("EnrollmentSeq", schema: "fauzansaef")
        //     .StartsAt(1)
        //     .IncrementsBy(1);
        //
        // modelBuilder.HasSequence<int>("CourseSeq", schema: "fauzansaef")
        //     .StartsAt(1)
        //     .IncrementsBy(1);
        
        modelBuilder.HasDefaultSchema("fauzansaef");
        modelBuilder.Entity<Student>(e =>
        {
            
            e.HasKey(x => x.ID);
            // e.Property(x => x.ID)
            //     .HasDefaultValueSql("NEXT VALUE FOR fauzansaef.StudentSeq");
            e.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAdd();
            e.HasMany(x=>x.Enrollments)
                .WithOne(x=>x.Student)
                .HasForeignKey(x=>x.StudentID);
            e.HasQueryFilter(x=>x.DeletedAt == null);
            
        });

        modelBuilder.Entity<Enrollment>(e =>
        {
            e.HasKey(x=>x.EnrollmentID);
            // e.Property(x => x.EnrollmentID)
            //     .HasDefaultValueSql("NEXT VALUE FOR fauzansaef.EnrollmentSeq");
            e.Property(x => x.StudentID).IsRequired();
            e.Property(x=>x.CourseID).IsRequired();
            e.HasOne(x=>x.Student)
                .WithMany(x=>x.Enrollments)
                .HasForeignKey(x=>x.StudentID);
            e.HasOne(x=>x.Course)
                .WithMany(x=>x.Enrollments)
                .HasForeignKey(x=>x.CourseID);
            e.HasQueryFilter(x=>x.DeletedAt == null);
        });

        modelBuilder.Entity<Course>(e =>
        {
            e.HasKey(x=>x.CourseID);
            // e.Property(x => x.CourseID)
            //     .HasDefaultValueSql("NEXT VALUE FOR fauzansaef.CourseSeq");
            e.Property(x=>x.Credits).IsRequired();
            e.Property(x=>x.Title).IsRequired().HasMaxLength(512);
            e.HasMany(x=>x.Enrollments)
                .WithOne(x=>x.Course)
                .HasForeignKey(x=>x.CourseID);
            e.HasQueryFilter(x=>x.DeletedAt == null);
        });
    }

    public void SavingChangeEvent(object? sender, SavingChangesEventArgs a)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if(entry.Entity is BaseEntity entity && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entity.DeletedAt = DateTime.Now;
            }
            
        }
    }
    
}