using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddSector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5001f34e-17f0-4e5e-922f-38bd9c2ad862");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b92b976b-ed54-499a-a125-4c2898365079");

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "BooksShelves",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "26249137-f07e-410b-8e3b-77ddd032eace", "65422b7a-832a-4feb-b6b6-4f0881fd1f14", "Super Administrator", "SUPER ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b63c1138-ce95-432b-bbd1-3af78006fdd5", "429ca267-8d5b-4ba8-b226-5762bbe1366d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_BooksShelves_SectorId",
                table: "BooksShelves",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksShelves_Sectors_SectorId",
                table: "BooksShelves",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksShelves_Sectors_SectorId",
                table: "BooksShelves");

            migrationBuilder.DropIndex(
                name: "IX_BooksShelves_SectorId",
                table: "BooksShelves");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26249137-f07e-410b-8e3b-77ddd032eace");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b63c1138-ce95-432b-bbd1-3af78006fdd5");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "BooksShelves");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b92b976b-ed54-499a-a125-4c2898365079", "c13c1f51-23a4-40b8-a405-f1bbabb3f6e4", "Super Administrator", "SUPER ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5001f34e-17f0-4e5e-922f-38bd9c2ad862", "8500c715-5f8b-4f36-aa75-8215caa7d005", "Administrator", "ADMINISTRATOR" });
        }
    }
}
