using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class RemoveNullable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Sections__Sector__5CA1C101",
                table: "Sections");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42a99866-dcbf-44b9-a9d8-9bb5419d7a32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c14cb1ba-34f5-4a76-aa8e-20772cdcfe20");

            migrationBuilder.AlterColumn<int>(
                name: "SectorId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0296bafe-0332-40c3-a63e-ea3399edfc66", "eb0892bd-c7f4-42d1-81dd-5eaea6076a12", "Super Administrator", "SUPER ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cde35e83-d5ed-4fcc-bab0-c2d49c5ad7f9", "ddee6c11-2a8e-41b3-92e6-3bbdccd82c8d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK__Sections__Sector__5CA1C101",
                table: "Sections",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Sections__Sector__5CA1C101",
                table: "Sections");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0296bafe-0332-40c3-a63e-ea3399edfc66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cde35e83-d5ed-4fcc-bab0-c2d49c5ad7f9");

            migrationBuilder.AlterColumn<int>(
                name: "SectorId",
                table: "Sections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c14cb1ba-34f5-4a76-aa8e-20772cdcfe20", "f01f0f1b-e02c-41a7-bb1c-fc4b57e0f923", "Super Administrator", "SUPER ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42a99866-dcbf-44b9-a9d8-9bb5419d7a32", "460b1787-a74e-415c-b21c-12a03a278295", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK__Sections__Sector__5CA1C101",
                table: "Sections",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
