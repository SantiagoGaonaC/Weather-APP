using System;
using System.Collections.Generic;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.EntityFrameworkCore;

namespace BackendWeatherAPP.Data;

public partial class WeatherDbContext : DbContext
{
    public WeatherDbContext()
    {
    }

    public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Sesione> Sesiones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ciudades__3213E83F5B5F1FF8");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Lat).HasColumnName("lat");
            entity.Property(e => e.Lon).HasColumnName("lon");
            entity.Property(e => e.NombreCiudad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre_ciudad");
        });

        modelBuilder.Entity<Sesione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sesiones__3213E83F93C8F4C4");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Sesiones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sesiones__id_usu__3A81B327");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83F60837346");

            entity.HasIndex(e => e.Username, "UQ__Usuarios__F3DBC572DF7AE290").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasMany(d => d.IdCiudads).WithMany(p => p.IdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuariosCiudade",
                    r => r.HasOne<Ciudade>().WithMany()
                        .HasForeignKey("IdCiudad")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Usuarios___id_ci__403A8C7D"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Usuarios___id_us__3F466844"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdCiudad").HasName("PK__Usuarios__0543C0606B55AB0E");
                        j.ToTable("Usuarios_Ciudades");
                        j.IndexerProperty<int>("IdUsuario").HasColumnName("id_usuario");
                        j.IndexerProperty<int>("IdCiudad").HasColumnName("id_ciudad");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
