namespace COMP2007_AS1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EventStaffModel : DbContext
    {
        public EventStaffModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>()
                .Property(e => e.positionName)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.hourlyPay)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Position>()
                .Property(e => e.duties)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Staffs)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.shiftHours)
                .HasPrecision(4, 2);
        }
    }
}
