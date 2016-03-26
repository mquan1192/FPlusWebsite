namespace fpluswebsite2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class fplus1 : DbContext
    {
        public fplus1()
            : base("name=fplus1")
        {
        }

        public virtual DbSet<Cleaning> Cleanings { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Pickup> Pickups { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cleaning>()
                .Property(e => e.Fee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.OwnerPhone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Cleanings)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.Deposit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Cleanings)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<fpluswebsite2.Models.User> Users { get; set; }
    }
}
