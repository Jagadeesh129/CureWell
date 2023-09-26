using System;
using CureWellUsingEF.Repos.Models;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Repos;

public partial class CureWellDBContext : DbContext
{
    public CureWellDBContext()
    {
    }

    public CureWellDBContext(DbContextOptions<CureWellDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Surgery> Surgeries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=INL674;Database=CurewellDB;Trusted_Connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EDFA3C2A9FA");
        });

        modelBuilder.Entity<DoctorSpecialization>(entity =>
        {
            entity.HasKey(e => new { e.DoctorId, e.SpecializationCode }).HasName("PK__DoctorSp__D3EF422B655E8694");

            entity.Property(e => e.SpecializationCode).IsFixedLength();

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSpecializations).HasConstraintName("FK__DoctorSpe__Docto__4D94879B");

            entity.HasOne(d => d.SpecializationCodeNavigation).WithMany(p => p.DoctorSpecializations).HasConstraintName("FK__DoctorSpe__Speci__4E88ABD4");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__RefreshT__CB9A1CFF0882BAD5");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationCode).HasName("PK__Speciali__E2F4CF4E3FFD7603");

            entity.Property(e => e.SpecializationCode).IsFixedLength();
        });

        modelBuilder.Entity<Surgery>(entity =>
        {
            entity.HasKey(e => e.SurgeryId).HasName("PK__Surgery__08AD55DDF5BF8B8C");

            entity.Property(e => e.SurgeryCategory).IsFixedLength();

            entity.HasOne(d => d.Doctor).WithMany(p => p.Surgeries)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Surgery__DoctorI__534D60F1");

            entity.HasOne(d => d.SurgeryCategoryNavigation).WithMany(p => p.Surgeries)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Surgery__Surgery__5441852A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK___User__3214EC07D65ABFA1");

            entity.Property(e => e.Mobile).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
