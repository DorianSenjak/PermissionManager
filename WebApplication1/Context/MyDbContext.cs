using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Migrations;

namespace WebApplication1.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<App_Baza> App_Bazas { get; set; }

    public virtual DbSet<App_Baza_Okolina> App_Baza_Okolinas { get; set; }

    public virtual DbSet<Baza> Bazas { get; set; }

    public virtual DbSet<Okolina> Okolinas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=BankDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<App>(entity =>
        {
            entity.HasKey(e => e.AppID).HasName("PK__App__8E2CF7D90C92F622");

            entity.ToTable("App");

            entity.Property(e => e.AppName).HasMaxLength(25);
        });

        modelBuilder.Entity<App_Baza>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_Baza");

            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.IDAppNavigation).WithMany()
                .HasForeignKey(d => d.IDApp)
                .HasConstraintName("FK__App_Baza__IDApp__4E88ABD4");

            entity.HasOne(d => d.IDBazaNavigation).WithMany()
                .HasForeignKey(d => d.IDBaza)
                .HasConstraintName("FK__App_Baza__IDBaza__4F7CD00D");
        });

        modelBuilder.Entity<App_Baza_Okolina>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("App_Baza_Okolina");

            entity.Property(e => e.Link).HasMaxLength(50);

            entity.HasOne(d => d.IDAppNavigation).WithMany()
                .HasForeignKey(d => d.IDApp)
                .HasConstraintName("FK__App_Baza___IDApp__5FB337D6");

            entity.HasOne(d => d.IDBazaNavigation).WithMany()
                .HasForeignKey(d => d.IDBaza)
                .HasConstraintName("FK__App_Baza___IDBaz__60A75C0F");

            entity.HasOne(d => d.IDOKLNavigation).WithMany()
                .HasForeignKey(d => d.IDOKL)
                .HasConstraintName("FK__App_Baza___IDOKL__619B8048");
        });

        modelBuilder.Entity<Baza>(entity =>
        {
            entity.HasKey(e => e.BazaID).HasName("PK__Baza__BEC41B4FECB7A9DD");

            entity.ToTable("Baza");

            entity.Property(e => e.BazaName).HasMaxLength(25);
        });

        modelBuilder.Entity<Okolina>(entity =>
        {
            entity.HasKey(e => e.OKLID).HasName("PK__Okolina__76145ED8FCC647A2");

            entity.ToTable("Okolina");

            entity.Property(e => e.OKLName).HasMaxLength(25);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
