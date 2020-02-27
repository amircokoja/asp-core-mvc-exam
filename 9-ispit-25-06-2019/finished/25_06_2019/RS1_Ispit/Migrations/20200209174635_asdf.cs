using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class asdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IspitStavke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IspitId = table.Column<int>(nullable: false),
                    Ocjena = table.Column<int>(nullable: false),
                    Pristupio = table.Column<bool>(nullable: false),
                    SlusaPredmetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IspitStavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IspitStavke_Ispit_IspitId",
                        column: x => x.IspitId,
                        principalTable: "Ispit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IspitStavke_SlusaPredmet_SlusaPredmetId",
                        column: x => x.SlusaPredmetId,
                        principalTable: "SlusaPredmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IspitStavke_IspitId",
                table: "IspitStavke",
                column: "IspitId");

            migrationBuilder.CreateIndex(
                name: "IX_IspitStavke_SlusaPredmetId",
                table: "IspitStavke",
                column: "SlusaPredmetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IspitStavke");
        }
    }
}
