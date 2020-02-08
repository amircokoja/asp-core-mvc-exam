using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class inicijalna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rezultlat",
                table: "PopravniIspitUcenik",
                newName: "Rezultat");

            migrationBuilder.RenameColumn(
                name: "PopravniIspitUcenikId",
                table: "PopravniIspitUcenik",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PopravniIspitId",
                table: "PopravniIspit",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rezultat",
                table: "PopravniIspitUcenik",
                newName: "Rezultlat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PopravniIspitUcenik",
                newName: "PopravniIspitUcenikId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PopravniIspit",
                newName: "PopravniIspitId");
        }
    }
}
