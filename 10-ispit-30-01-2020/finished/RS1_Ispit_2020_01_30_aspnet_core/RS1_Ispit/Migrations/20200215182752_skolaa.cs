using Microsoft.EntityFrameworkCore.Migrations;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class skolaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkolaID",
                table: "Takmicenje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Takmicenje_SkolaID",
                table: "Takmicenje",
                column: "SkolaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Takmicenje_Skola_SkolaID",
                table: "Takmicenje",
                column: "SkolaID",
                principalTable: "Skola",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Takmicenje_Skola_SkolaID",
                table: "Takmicenje");

            migrationBuilder.DropIndex(
                name: "IX_Takmicenje_SkolaID",
                table: "Takmicenje");

            migrationBuilder.DropColumn(
                name: "SkolaID",
                table: "Takmicenje");
        }
    }
}
