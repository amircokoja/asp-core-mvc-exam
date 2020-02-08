using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ispit_2017_02_15.Migrations
{
    public partial class login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogId",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogId",
                table: "Nastavnik",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KorisnickiNalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_KorisnickiNalogId",
                table: "Student",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Nastavnik_KorisnickiNalogId",
                table: "Nastavnik",
                column: "KorisnickiNalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nastavnik_KorisnickiNalog_KorisnickiNalogId",
                table: "Nastavnik",
                column: "KorisnickiNalogId",
                principalTable: "KorisnickiNalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_KorisnickiNalog_KorisnickiNalogId",
                table: "Student",
                column: "KorisnickiNalogId",
                principalTable: "KorisnickiNalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nastavnik_KorisnickiNalog_KorisnickiNalogId",
                table: "Nastavnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_KorisnickiNalog_KorisnickiNalogId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "KorisnickiNalog");

            migrationBuilder.DropIndex(
                name: "IX_Student_KorisnickiNalogId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Nastavnik_KorisnickiNalogId",
                table: "Nastavnik");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogId",
                table: "Nastavnik");
        }
    }
}
