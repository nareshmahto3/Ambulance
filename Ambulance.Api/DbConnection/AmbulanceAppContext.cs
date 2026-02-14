using System;
using System.Collections.Generic;
using Ambulance.Api.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Api.DbConnection;

public partial class AmbulanceAppContext : DbContext
{
    public AmbulanceAppContext()
    {
    }

    public AmbulanceAppContext(DbContextOptions<AmbulanceAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleDeviceAdmin> VehicleDeviceAdmins { get; set; }

    public virtual DbSet<VehicleInsurance> VehicleInsurances { get; set; }

    public virtual DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }

    public virtual DbSet<VehicleRegistration> VehicleRegistrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=198.38.89.21;Database=AmbulanceApp;user id=Ambulance_User;password=Ambulance@123;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B5492F726BA27");

            entity.Property(e => e.ChassisNumber).HasMaxLength(50);
            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EngineNumber).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.RegistrationNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<VehicleDeviceAdmin>(entity =>
        {
            entity.HasKey(e => e.VehicleDeviceAdminId).HasName("PK__VehicleD__72E9B3A3116495E7");

            entity.ToTable("VehicleDeviceAdmin");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Imeinumber)
                .HasMaxLength(50)
                .HasColumnName("IMEINumber");
            entity.Property(e => e.OwnerMobileNumber).HasMaxLength(15);
            entity.Property(e => e.OwnerName).HasMaxLength(150);

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleDeviceAdmins)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehicleDeviceAdmin_Vehicles");
        });

        modelBuilder.Entity<VehicleInsurance>(entity =>
        {
            entity.HasKey(e => e.VehicleInsuranceId).HasName("PK__VehicleI__750E9132771EC077");

            entity.ToTable("VehicleInsurance");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.InsuranceNumber).HasMaxLength(100);
            entity.Property(e => e.InsuranceProvider).HasMaxLength(150);

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleInsurances)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehicleInsurance_Vehicles");
        });

        modelBuilder.Entity<VehicleMaintenance>(entity =>
        {
            entity.HasKey(e => e.VehicleMaintenanceId).HasName("PK__VehicleM__99FA6BA7A5767FE0");

            entity.ToTable("VehicleMaintenance");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FuelCalibrationMaximum).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FuelCalibrationMinimum).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LastPuccheckDate).HasColumnName("LastPUCCheckDate");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleMaintenances)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehicleMaintenance_Vehicles");
        });

        modelBuilder.Entity<VehicleRegistration>(entity =>
        {
            entity.HasKey(e => e.VehicleRegistrationId).HasName("PK__VehicleR__42563268CA6B6D74");

            entity.ToTable("VehicleRegistration");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FuelType).HasMaxLength(50);
            entity.Property(e => e.PermitLevel).HasMaxLength(100);
            entity.Property(e => e.RcvalidUpto).HasColumnName("RCValidUpto");
            entity.Property(e => e.VehicleClass).HasMaxLength(50);

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleRegistrations)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehicleRegistration_Vehicles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
