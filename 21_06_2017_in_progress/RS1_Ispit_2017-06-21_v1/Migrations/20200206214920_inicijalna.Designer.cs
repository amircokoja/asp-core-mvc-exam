﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RS1_Ispit_2017_06_21_v1.EF;
using System;

namespace RS1_Ispit_20170621_v1.Migrations
{
    [DbContext(typeof(MojContext))]
    [Migration("20200206214920_inicijalna")]
    partial class inicijalna
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RS1_Ispit_2017_06_21_v1.Models.Nastavnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImePrezime");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Nastavnik");
                });

            modelBuilder.Entity("RS1_Ispit_2017_06_21_v1.Models.Odjeljenje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NastavnikId");

                    b.Property<string>("Naziv");

                    b.HasKey("Id");

                    b.HasIndex("NastavnikId");

                    b.ToTable("Odjeljenje");
                });

            modelBuilder.Entity("RS1_Ispit_2017_06_21_v1.Models.Ucenik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImePrezime");

                    b.HasKey("Id");

                    b.ToTable("Ucenik");
                });

            modelBuilder.Entity("RS1_Ispit_2017_06_21_v1.Models.UpisUOdjeljenje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrojUDnevniku");

                    b.Property<int>("OdjeljenjeId");

                    b.Property<int>("OpciUspjeh");

                    b.Property<int>("UcenikId");

                    b.HasKey("Id");

                    b.HasIndex("OdjeljenjeId");

                    b.HasIndex("UcenikId");

                    b.ToTable("UpisUOdjeljenje");
                });

            modelBuilder.Entity("RS1_Ispit_2017_06_21_v1.Models.Odjeljenje", b =>
                {
                    b.HasOne("RS1_Ispit_2017_06_21_v1.Models.Nastavnik", "Nastavnik")
                        .WithMany()
                        .HasForeignKey("NastavnikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RS1_Ispit_2017_06_21_v1.Models.UpisUOdjeljenje", b =>
                {
                    b.HasOne("RS1_Ispit_2017_06_21_v1.Models.Odjeljenje", "Odjeljenje")
                        .WithMany()
                        .HasForeignKey("OdjeljenjeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RS1_Ispit_2017_06_21_v1.Models.Ucenik", "Ucenik")
                        .WithMany()
                        .HasForeignKey("UcenikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
