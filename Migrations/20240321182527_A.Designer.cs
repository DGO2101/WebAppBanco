﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppBanco.Data;

#nullable disable

namespace WebAppBanco4.Migrations
{
    [DbContext(typeof(WebAppBancoContext))]
    [Migration("20240321182527_A")]
    partial class A
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ComprasTarjetas", b =>
                {
                    b.Property<int>("ComprasId")
                        .HasColumnType("int");

                    b.Property<int>("TarjetasId")
                        .HasColumnType("int");

                    b.HasKey("ComprasId", "TarjetasId");

                    b.HasIndex("TarjetasId");

                    b.ToTable("ComprasTarjetas");
                });

            modelBuilder.Entity("PagosTarjetas", b =>
                {
                    b.Property<int>("PagosId")
                        .HasColumnType("int");

                    b.Property<int>("TarjetasId")
                        .HasColumnType("int");

                    b.HasKey("PagosId", "TarjetasId");

                    b.HasIndex("TarjetasId");

                    b.ToTable("PagosTarjetas");
                });

            modelBuilder.Entity("WebAppBanco.Models.Compras", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("WebAppBanco.Models.Pagos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("WebAppBanco.Models.Tarjetas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tarjetas");
                });

            modelBuilder.Entity("ComprasTarjetas", b =>
                {
                    b.HasOne("WebAppBanco.Models.Compras", null)
                        .WithMany()
                        .HasForeignKey("ComprasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppBanco.Models.Tarjetas", null)
                        .WithMany()
                        .HasForeignKey("TarjetasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PagosTarjetas", b =>
                {
                    b.HasOne("WebAppBanco.Models.Pagos", null)
                        .WithMany()
                        .HasForeignKey("PagosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppBanco.Models.Tarjetas", null)
                        .WithMany()
                        .HasForeignKey("TarjetasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
