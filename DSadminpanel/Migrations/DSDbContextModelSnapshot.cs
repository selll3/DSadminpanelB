﻿// <auto-generated />
using System;
using DSadminpanel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DSadminpanel.Migrations
{
    [DbContext(typeof(DSDbContext))]
    partial class DSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DSadminpanel.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("DSadminpanel.Models.modified8", b =>
                {
                    b.Property<int>("urunid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("urunid"));

                    b.Property<string>("Ana_kategori")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Birim")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("Birim_fiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DVZ")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Etiketler")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("KDV")
                        .HasColumnType("float");

                    b.Property<int?>("Kritik_stok_miktarı")
                        .HasColumnType("int");

                    b.Property<double?>("Kutu_Miktarı")
                        .HasColumnType("float");

                    b.Property<string>("Marka")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MİKTAR")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Stok")
                        .HasColumnType("int");

                    b.Property<bool?>("Stok_kullanır")
                        .HasColumnType("bit");

                    b.Property<string>("TEMİN")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("TESLİM")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Ürün_kodu")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal?>("İSK")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("İsim")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.HasKey("urunid");

                    b.ToTable("modified8s");
                });

            modelBuilder.Entity("DSadminpanel.Models.musteri", b =>
                {
                    b.Property<int>("musteri_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("musteri_id"));

                    b.Property<string>("ad_soyad_firma")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("telefon")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("musteri_id");

                    b.ToTable("Musteriler");
                });

            modelBuilder.Entity("DSadminpanel.Models.teklif", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("Musteri_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("gecerlilik_tarihi")
                        .HasColumnType("date");

                    b.Property<decimal?>("genel_toplam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("kdv")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("musteriid")
                        .HasColumnType("int");

                    b.Property<DateTime>("teklif_tarihi")
                        .HasColumnType("date");

                    b.Property<decimal?>("toplam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("toplam_euro")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("toplam_tl")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("toplam_usd")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("Musteri_id");

                    b.ToTable("teklifs");
                });

            modelBuilder.Entity("DSadminpanel.Models.teklif_urunleri", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<decimal?>("birim_fiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("iskonto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("miktar")
                        .HasColumnType("int");

                    b.Property<int>("teklif_id")
                        .HasColumnType("int");

                    b.Property<string>("teslim_suresi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("tutar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("urun_id")
                        .HasColumnType("int");

                    b.Property<int>("urunid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("urunid");

                    b.ToTable("teklif_Urunleris");
                });

            modelBuilder.Entity("DSadminpanel.Models.teklif", b =>
                {
                    b.HasOne("DSadminpanel.Models.musteri", "musteri")
                        .WithMany("teklif")
                        .HasForeignKey("Musteri_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("musteri");
                });

            modelBuilder.Entity("DSadminpanel.Models.teklif_urunleri", b =>
                {
                    b.HasOne("DSadminpanel.Models.teklif", "teklif")
                        .WithMany("TeklifUrunleri")
                        .HasForeignKey("id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DSadminpanel.Models.modified8", "Modified8")
                        .WithMany("teklif_urunleri")
                        .HasForeignKey("urunid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modified8");

                    b.Navigation("teklif");
                });

            modelBuilder.Entity("DSadminpanel.Models.modified8", b =>
                {
                    b.Navigation("teklif_urunleri");
                });

            modelBuilder.Entity("DSadminpanel.Models.musteri", b =>
                {
                    b.Navigation("teklif");
                });

            modelBuilder.Entity("DSadminpanel.Models.teklif", b =>
                {
                    b.Navigation("TeklifUrunleri");
                });
#pragma warning restore 612, 618
        }
    }
}
