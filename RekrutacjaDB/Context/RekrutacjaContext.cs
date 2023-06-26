using Microsoft.EntityFrameworkCore;
using Rekrutacja.Models;

namespace Rekrutacja.Context;

public partial class RekrutacjaContext : DbContext
{
    public virtual DbSet<Adresy> Adresy { get; set; }

    public virtual DbSet<Dokumenty> Dokumenty { get; set; }

    public virtual DbSet<Egzaminy> Egzaminy { get; set; }

    public virtual DbSet<Kandydaci> Kandydaci { get; set; }

    public virtual DbSet<Kierunki> Kierunki { get; set; }

    public virtual DbSet<Pracownicy> Pracownicy { get; set; }

    public virtual DbSet<Płatności> Płatności { get; set; }

    public virtual DbSet<Użytkownicy> Użytkownicy { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlServer("Data Source=LAPTOP-HA5MSFQO\\SQLEXPRESS;Database=Rekrutacja;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Użytkownicy>()
            .HasMany(u => u.Adresy)
            .WithOne(a => a.Użytkownik)
            .HasForeignKey(a => a.UżytkownikId);

        modelBuilder.Entity<Użytkownicy>()
            .HasOne(u => u.Pracownicy)
            .WithOne(p => p.Użytkownik)
            .HasForeignKey<Pracownicy>(p => p.UżytkownikId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Dokumenty>()
            .HasOne(d => d.Pracownicy)
            .WithMany(p => p.DokumentyPracowników)
            .HasForeignKey(d => d.PracownicyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Kandydaci>()
            .HasOne(k => k.Użytkownik)
            .WithOne(u => u.Kandydaci)
            .HasForeignKey<Kandydaci>(k => k.UżytkownikId);

        modelBuilder.Entity<Kandydaci>()
            .HasOne(k => k.Kierunek)
            .WithOne(ki => ki.Kandydaci)
            .HasForeignKey<Kandydaci>(k => k.KierunekId);

        modelBuilder.Entity<Kandydaci>()
            .HasMany(k => k.Płatności)
            .WithOne(pł => pł.Kandydat)
            .HasForeignKey(pł => pł.KandydatId);

        modelBuilder.Entity<Kandydaci>()
            .HasMany(k => k.Egzaminy)
            .WithOne(e => e.Kandydat)
            .HasForeignKey(e => e.KandydatId);

        modelBuilder.Entity<Kandydaci>()
            .HasMany(k => k.Dokumenty)
            .WithOne(d => d.Kandydaci)
            .HasForeignKey(d => d.KandydatId);
    }
}
