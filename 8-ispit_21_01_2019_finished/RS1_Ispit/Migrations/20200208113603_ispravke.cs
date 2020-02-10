﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class ispravke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaturskiIspit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    PredmetId = table.Column<int>(nullable: false),
                    SkolaId = table.Column<int>(nullable: false),
                    SkolskaGodinaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Nastavnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Skola_SkolaId",
                        column: x => x.SkolaId,
                        principalTable: "Skola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_SkolskaGodina_SkolskaGodinaId",
                        column: x => x.SkolskaGodinaId,
                        principalTable: "SkolskaGodina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaturskiIspitStavke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaturskiIspitId = table.Column<int>(nullable: false),
                    OdjeljenjeStavkaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspitStavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavke_MaturskiIspit_MaturskiIspitId",
                        column: x => x.MaturskiIspitId,
                        principalTable: "MaturskiIspit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavke_OdjeljenjeStavka_OdjeljenjeStavkaId",
                        column: x => x.OdjeljenjeStavkaId,
                        principalTable: "OdjeljenjeStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_NastavnikId",
                table: "MaturskiIspit",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_PredmetId",
                table: "MaturskiIspit",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_SkolaId",
                table: "MaturskiIspit",
                column: "SkolaId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_SkolskaGodinaId",
                table: "MaturskiIspit",
                column: "SkolskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavke_MaturskiIspitId",
                table: "MaturskiIspitStavke",
                column: "MaturskiIspitId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavke_OdjeljenjeStavkaId",
                table: "MaturskiIspitStavke",
                column: "OdjeljenjeStavkaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaturskiIspitStavke");

            migrationBuilder.DropTable(
                name: "MaturskiIspit");
        }
    }
}
