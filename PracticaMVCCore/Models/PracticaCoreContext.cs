using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PracticaMVCCore.Models
{
    public partial class PracticaCoreContext : DbContext
    {
        public PracticaCoreContext()
        {
        }

        public PracticaCoreContext(DbContextOptions<PracticaCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargo> Cargos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(e => e.IdCargo)
                    .HasName("PK__Cargo__6C9856257083CCEE");

                entity.ToTable("Cargo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__CE6D8B9E76D81CC5");

                entity.ToTable("Empleado");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.oCargo)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdCargo)
                    .HasConstraintName("FK_Empleado_ToTable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
