using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Models;

public partial class MedelStoreContext : DbContext
{
    public MedelStoreContext()
    {
    }

    public MedelStoreContext(DbContextOptions<MedelStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Categori> Categoris { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Mebel> Mebels { get; set; }

    public virtual DbSet<Pasport> Pasports { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Postavhik> Postavhiks { get; set; }

    public virtual DbSet<Postavki> Postavkis { get; set; }

    public virtual DbSet<SostavZakaza> SostavZakazas { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-9AHGKHV\\SQLEXPRESS;Initial Catalog=Medel_store;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.IdAdress).HasName("PK__Adress__C0B17B217A918882");

            entity.ToTable("Adress");

            entity.Property(e => e.IdAdress).HasColumnName("ID_adress");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Street)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.IdApplications).HasName("PK__Applicat__730CFFBD524D2BA2");

            entity.Property(e => e.IdApplications).HasColumnName("ID_applications");
            entity.Property(e => e.ApplicationStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("application_status");
            entity.Property(e => e.ClientsId).HasColumnName("clients_ID");
            entity.Property(e => e.DateOfApplicationSubmission).HasColumnName("date_of_application_submission");
            entity.Property(e => e.MebelId).HasColumnName("mebel_ID");
            entity.Property(e => e.StaffId).HasColumnName("staff_ID");

            entity.HasOne(d => d.Clients).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ClientsId)
                .HasConstraintName("FK__Applicati__clien__534D60F1");

            entity.HasOne(d => d.Mebel).WithMany(p => p.Applications)
                .HasForeignKey(d => d.MebelId)
                .HasConstraintName("FK__Applicati__mebel__52593CB8");

            entity.HasOne(d => d.Staff).WithMany(p => p.Applications)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Applicati__staff__5441852A");
        });

        modelBuilder.Entity<Categori>(entity =>
        {
            entity.HasKey(e => e.IdCategori).HasName("PK__Categori__AC5CA41EE511BFFD");

            entity.ToTable("Categori");

            entity.Property(e => e.IdCategori).HasColumnName("ID_categori");
            entity.Property(e => e.NameCategori)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name_categori");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClients).HasName("PK__Clients__9BB2654543ECCE85");

            entity.Property(e => e.IdClients).HasColumnName("ID_clients");
            entity.Property(e => e.AdressId).HasColumnName("adress_ID");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Fatherland)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fatherland");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PassportId).HasColumnName("passport_ID");
            entity.Property(e => e.Suname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("suname");

            entity.HasOne(d => d.Adress).WithMany(p => p.Clients)
                .HasForeignKey(d => d.AdressId)
                .HasConstraintName("FK__Clients__adress___3F466844");

            entity.HasOne(d => d.Passport).WithMany(p => p.Clients)
                .HasForeignKey(d => d.PassportId)
                .HasConstraintName("FK__Clients__passpor__403A8C7D");
        });

        modelBuilder.Entity<Mebel>(entity =>
        {
            entity.HasKey(e => e.IdMebel).HasName("PK__Mebel__CA7A3969D9C3EA4A");

            entity.ToTable("Mebel");

            entity.Property(e => e.IdMebel).HasColumnName("ID_mebel");
            entity.Property(e => e.CategoriId).HasColumnName("categori_ID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("product_name");

            entity.HasOne(d => d.Categori).WithMany(p => p.Mebels)
                .HasForeignKey(d => d.CategoriId)
                .HasConstraintName("FK__Mebel__categori___4316F928");
        });

        modelBuilder.Entity<Pasport>(entity =>
        {
            entity.HasKey(e => e.IdPasports).HasName("PK__Pasports__FBF6E1C1D21F25AF");

            entity.Property(e => e.IdPasports).HasColumnName("ID_pasports");
            entity.Property(e => e.DateOfIssue).HasColumnName("date_of_issue");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Serial).HasColumnName("serial");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.IdPositions).HasName("PK__Position__AD4231AB25948F80");

            entity.Property(e => e.IdPositions).HasColumnName("ID_positions");
            entity.Property(e => e.Positions)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("positions");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<Postavhik>(entity =>
        {
            entity.HasKey(e => e.IdPostavchik).HasName("PK__Postavhi__287BBC75D7E99CB1");

            entity.ToTable("Postavhik");

            entity.Property(e => e.IdPostavchik).HasColumnName("ID_postavchik");
            entity.Property(e => e.Contact)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("contact");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Postavki>(entity =>
        {
            entity.HasKey(e => e.IdPostavki).HasName("PK__Postavki__5C79085BF7D455D4");

            entity.ToTable("Postavki");

            entity.Property(e => e.IdPostavki).HasColumnName("ID_postavki");
            entity.Property(e => e.DatePostavki).HasColumnName("date_postavki");
            entity.Property(e => e.MebelId).HasColumnName("mebel_ID");
            entity.Property(e => e.PostavchikaId).HasColumnName("postavchika_ID");
            entity.Property(e => e.PricePostavki)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Price_postavki");

            entity.HasOne(d => d.Mebel).WithMany(p => p.Postavkis)
                .HasForeignKey(d => d.MebelId)
                .HasConstraintName("FK__Postavki__mebel___46E78A0C");

            entity.HasOne(d => d.Postavchika).WithMany(p => p.Postavkis)
                .HasForeignKey(d => d.PostavchikaId)
                .HasConstraintName("FK__Postavki__postav__45F365D3");
        });

        modelBuilder.Entity<SostavZakaza>(entity =>
        {
            entity.HasKey(e => e.IdSostavZakaza).HasName("PK__Sostav_z__A5C1194A44AA0E25");

            entity.ToTable("Sostav_zakaza");

            entity.Property(e => e.IdSostavZakaza).HasColumnName("ID_sostav_zakaza");
            entity.Property(e => e.MebelId).HasColumnName("mebel_ID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Mebel).WithMany(p => p.SostavZakazas)
                .HasForeignKey(d => d.MebelId)
                .HasConstraintName("FK__Sostav_za__mebel__49C3F6B7");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.IdStaff).HasName("PK__Staff__E04EF0472CF3C7BD");

            entity.Property(e => e.IdStaff).HasColumnName("ID_staff");
            entity.Property(e => e.Fatherland)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fatherland");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PassportId).HasColumnName("passport_ID");
            entity.Property(e => e.PositionsId).HasColumnName("positions_ID");
            entity.Property(e => e.Suname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("suname");

            entity.HasOne(d => d.Passport).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PassportId)
                .HasConstraintName("FK__Staff__passport___4E88ABD4");

            entity.HasOne(d => d.Positions).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PositionsId)
                .HasConstraintName("FK__Staff__positions__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
