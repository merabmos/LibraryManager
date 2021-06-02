using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class UpdateRoleAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39129d89-2588-4bff-aa07-12516c83d3ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4323f38f-cf58-4783-a7f2-03f1f5496055");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b92b976b-ed54-499a-a125-4c2898365079", "c13c1f51-23a4-40b8-a405-f1bbabb3f6e4", "Super Administrator", "SUPER ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5001f34e-17f0-4e5e-922f-38bd9c2ad862", "8500c715-5f8b-4f36-aa75-8215caa7d005", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5001f34e-17f0-4e5e-922f-38bd9c2ad862");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b92b976b-ed54-499a-a125-4c2898365079");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4323f38f-cf58-4783-a7f2-03f1f5496055", "b8f9d52d-8fcf-468e-bca1-1433e2d94192", "Super Administrator", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39129d89-2588-4bff-aa07-12516c83d3ed", "e519cf7e-7635-45c2-ba67-c10b53d62f41", "Administrator", null });
        }
    }
}
