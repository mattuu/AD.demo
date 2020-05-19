using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AD.Demo.DataAccess
{
    public partial class TechTestContext : DbContext
    {
        public TechTestContext(DbContextOptions<TechTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Colours> Colours { get; set; }
        public virtual DbSet<FavouriteColours> FavouriteColours { get; set; }
        public virtual DbSet<People> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                // optionsBuilder.UseSqlServer("Data Source=XPS15\\SQLEXPRESS;Initial Catalog=TechTest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colours>(entity =>
            {
                entity.HasKey(e => e.ColourId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FavouriteColours>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.ColourId });

                entity.HasOne(d => d.Colour)
                    .WithMany(p => p.FavouriteColours)
                    .HasForeignKey(d => d.ColourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteColours_Colours");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.FavouriteColours)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteColours_People");
            });

            modelBuilder.Entity<People>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
