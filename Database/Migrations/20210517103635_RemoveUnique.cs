using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class RemoveUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__Sectors__737584F6D5CA3577",
                table: "Sectors");

            migrationBuilder.DropIndex(
                name: "UQ__Sections__737584F660D4979F",
                table: "Sections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UQ__Sectors__737584F6D5CA3577",
                table: "Sectors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Sections__737584F660D4979F",
                table: "Sections",
                column: "Name",
                unique: true);
        }
    }
}
