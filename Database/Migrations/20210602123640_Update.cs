using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4323f38f-cf58-4783-a7f2-03f1f5496055", "b8f9d52d-8fcf-468e-bca1-1433e2d94192", "Super Administrator", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39129d89-2588-4bff-aa07-12516c83d3ed", "e519cf7e-7635-45c2-ba67-c10b53d62f41", "Administrator", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39129d89-2588-4bff-aa07-12516c83d3ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4323f38f-cf58-4783-a7f2-03f1f5496055");
        }
    }
}
