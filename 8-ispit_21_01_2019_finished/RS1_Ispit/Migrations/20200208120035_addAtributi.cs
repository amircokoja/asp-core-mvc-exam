using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class addAtributi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pristupio",
                table: "MaturskiIspitStavke",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rezultat",
                table: "MaturskiIspitStavke",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pristupio",
                table: "MaturskiIspitStavke");

            migrationBuilder.DropColumn(
                name: "Rezultat",
                table: "MaturskiIspitStavke");
        }
    }
}
