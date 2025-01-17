﻿// <auto-generated />
using Ejercicio3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ejercicio3.Migrations
{
    [DbContext(typeof(SustitucionesContext))]
    [Migration("20201122223723_CreateSustitucionesDB")]
    partial class CreateSustitucionesDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Ejercicio3.Sustituciones", b =>
                {
                    b.Property<int>("SustitucionesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idPadre")
                        .HasColumnType("int");

                    b.HasKey("SustitucionesId");

                    b.ToTable("Sustituciones");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Sustituciones");
                });

            modelBuilder.Entity("Ejercicio3.SustitucionesCompuestas", b =>
                {
                    b.HasBaseType("Ejercicio3.Sustituciones");

                    b.HasDiscriminator().HasValue("SustitucionesCompuestas");
                });

            modelBuilder.Entity("Ejercicio3.SustitucionesTeclado", b =>
                {
                    b.HasBaseType("Ejercicio3.Sustituciones");

                    b.HasDiscriminator().HasValue("SustitucionesTeclado");
                });

            modelBuilder.Entity("Ejercicio3.SustitucionesTexto", b =>
                {
                    b.HasBaseType("Ejercicio3.Sustituciones");

                    b.HasDiscriminator().HasValue("SustitucionesTexto");
                });
#pragma warning restore 612, 618
        }
    }
}
