using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class IthealthContext : DbContext
{
    public IthealthContext()
    {
    }

    public IthealthContext(DbContextOptions<IthealthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasIndex(e => e.Code, "UQ__Exams__A25C5AA714A25647").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Orders_Patients");

            entity.HasMany(d => d.Exams).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderExam",
                    r => r.HasOne<Exam>().WithMany()
                        .HasForeignKey("ExamId")
                        .HasConstraintName("FK_OrderExams_Exams"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderExams_Orders"),
                    j =>
                    {
                        j.HasKey("OrderId", "ExamId");
                        j.ToTable("OrderExams");
                    });
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FullName).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
