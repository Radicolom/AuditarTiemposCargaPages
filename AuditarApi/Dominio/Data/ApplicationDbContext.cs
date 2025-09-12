using System;
using System.Collections.Generic;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dominio.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accion> Accions { get; set; }

    public virtual DbSet<AuditarLog> AuditarLogs { get; set; }

    public virtual DbSet<AuditarPagina> AuditarPaginas { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRol> MenuRols { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolOperacionAccion> RolOperacionAccions { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ConnectionAuditar");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accion>(entity =>
        {
            entity.HasKey(e => e.AccionId).HasName("PK__Accion__A60CAFC7F29FA1AC");

            entity.ToTable("Accion", "Configuracion");

            entity.Property(e => e.NombreAccion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AuditarLog>(entity =>
        {
            entity.HasKey(e => e.AuditarLogId).HasName("PK__AuditarL__B5AFA16C40361FC4");

            entity.ToTable("AuditarLog", "Pages");

            entity.Property(e => e.ClsValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FcpValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LcpValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpeedIndexValue)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AuditarPagina).WithMany(p => p.AuditarLogs)
                .HasForeignKey(d => d.AuditarPaginaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditarLog_AuditarPagina");
        });

        modelBuilder.Entity<AuditarPagina>(entity =>
        {
            entity.HasKey(e => e.AuditarPaginaId).HasName("PK__AuditarP__22CAB7527470E753");

            entity.ToTable("AuditarPagina", "Pages");

            entity.HasIndex(e => e.UrlAuditarPagina, "UQ__AuditarP__3E2798B2A7EDF1C0").IsUnique();

            entity.Property(e => e.EstadoAuditarPagina).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacionAuditarPagina)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreAuditarPagina)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UrlAuditarPagina)
                .HasMaxLength(1700)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.AuditarPaginas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_AuditarPagina_Usuario");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED230FF7C249D");

            entity.ToTable("Menu", "Configuracion");

            entity.Property(e => e.IconoMenu)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NombreMenu)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UrlMenu).IsUnicode(false);
        });

        modelBuilder.Entity<MenuRol>(entity =>
        {
            entity.HasKey(e => e.MenuRolId).HasName("PK__MenuRol__6640AD0C9286098E");

            entity.ToTable("MenuRol", "Configuracion");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_MenuRol_Menu");

            entity.HasOne(d => d.Rol).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_MenuRol_Rol");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302F135AE38BB");

            entity.ToTable("Rol", "Seguridad");

            entity.Property(e => e.NombreRol)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolOperacionAccion>(entity =>
        {
            entity.HasKey(e => e.RolOperacionAccionId).HasName("PK__RolOpera__D2B227DECE877EBC");

            entity.ToTable("RolOperacionAccion", "Seguridad");

            entity.HasOne(d => d.Accion).WithMany(p => p.RolOperacionAccions)
                .HasForeignKey(d => d.AccionId)
                .HasConstraintName("FK_RolOperacionAccion_Accion");

            entity.HasOne(d => d.Rol).WithMany(p => p.RolOperacionAccions)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_RolOperacionAccion_Rol");

            entity.HasOne(d => d.Servicio).WithMany(p => p.RolOperacionAccions)
                .HasForeignKey(d => d.ServicioId)
                .HasConstraintName("FK_RolOperacionAccion_Servicio");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioId).HasName("PK__Servicio__D5AEECC214E29B7B");

            entity.ToTable("Servicio", "Configuracion");

            entity.Property(e => e.NombreServicio)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B81C0B815A");

            entity.ToTable("Usuario", "Seguridad");

            entity.HasIndex(e => e.CorreoUsuario, "UQ__Usuario__365498782462CAFA").IsUnique();

            entity.HasIndex(e => e.DocumentoUsuario, "UQ__Usuario__ACE86E55C9CCE580").IsUnique();

            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DocumentoUsuario)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PasswordUsuario).IsUnicode(false);
            entity.Property(e => e.TelefonoUsuario)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
