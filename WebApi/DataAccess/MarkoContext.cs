using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DataAccess;

public partial class MarkoContext : DbContext
{
    public MarkoContext()
    {
    }

    public MarkoContext(DbContextOptions<MarkoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ability> Abilities { get; set; } = default!;

    public virtual DbSet<Address> Addresses { get; set; } = default!;

    public virtual DbSet<Person> People { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ability");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(5);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("person");

            entity.HasIndex(e => e.AddressId, "AddressId");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Address).WithMany(p => p.People)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_ibfk_1");

            entity.HasMany(d => d.Abilities).WithMany(p => p.People)
                .UsingEntity<Dictionary<string, object>>(
                    "Power",
                    r => r.HasOne<Ability>().WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("powers_ibfk_2"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("powers_ibfk_1"),
                    j =>
                    {
                        j.HasKey("PersonId", "AbilityId").HasName("PRIMARY");
                        j.ToTable("powers");
                        j.HasIndex(new[] { "AbilityId" }, "abilityId");
                        j.IndexerProperty<int>("PersonId").HasColumnName("personId");
                        j.IndexerProperty<int>("AbilityId").HasColumnName("abilityId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
