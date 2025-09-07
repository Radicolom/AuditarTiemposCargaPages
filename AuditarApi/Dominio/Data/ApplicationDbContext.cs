using System;
using System.Collections.Generic;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

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
            entity.HasKey(e => e.AccionId).HasName("PK__Accion__A60CAFC71DB83AD0");

            entity.ToTable("Accion", "Configuracion");

            entity.Property(e => e.NombreAccion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AuditarLog>(entity =>
        {
            entity.HasKey(e => e.AuditarLogId).HasName("PK__AuditarL__B5AFA16CCD2AFD4F");

            entity.ToTable("AuditarLog", "Pages");

            entity.Property(e => e.FechaCreacionAuditarLog)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AuditarPagina).WithMany(p => p.AuditarLogs)
                .HasForeignKey(d => d.AuditarPaginaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditarLog_AuditarPagina");
        });

        modelBuilder.Entity<AuditarPagina>(entity =>
        {
            entity.HasKey(e => e.AuditarPaginaId).HasName("PK__AuditarP__22CAB752501E6A94");

            entity.ToTable("AuditarPagina", "Pages");

            entity.HasIndex(e => e.UrlAuditarPagina, "UQ__AuditarP__3E2798B278130ECB").IsUnique();

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
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED2306AE753EB");

            entity.ToTable("Menu", "Configuracion");

            entity.Property(e => e.EstadoMenu).HasDefaultValue(true);
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
            entity.HasKey(e => e.MenuRolId).HasName("PK__MenuRol__6640AD0CF423469B");

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
            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302F15C41FE18");

            entity.ToTable("Rol", "Seguridad");

            entity.Property(e => e.EstadoRol).HasDefaultValue(true);
            entity.Property(e => e.NombreRol)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolOperacionAccion>(entity =>
        {
            entity.HasKey(e => e.RolOperacionAccionId).HasName("PK__RolOpera__D2B227DEFC9DB4BC");

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
            entity.HasKey(e => e.ServicioId).HasName("PK__Servicio__D5AEECC246E07806");

            entity.ToTable("Servicio", "Configuracion");

            entity.Property(e => e.NombreServicio)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B86BF034D7");

            entity.ToTable("Usuario", "Seguridad");

            entity.HasIndex(e => e.CorreoUsuario, "UQ__Usuario__365498780F7AD261").IsUnique();

            entity.HasIndex(e => e.DocumentoUsuario, "UQ__Usuario__ACE86E55D3BACC2B").IsUnique();

            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AutenticacionDobleFactor).HasDefaultValue(false);
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DocumentoUsuario)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EmailConfirmed).HasDefaultValue(false);
            entity.Property(e => e.EstadoUsuario).HasDefaultValue(true);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PasswordUsuario).IsUnicode(false);
            entity.Property(e => e.TelefonoConfirmadoUsuario).HasDefaultValue(false);
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
