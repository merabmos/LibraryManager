using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class RemoveNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c90484c-9d6a-415f-a5a7-a4a78ec4aa4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b767f674-3670-4bc9-b242-b5613f8efce6");


            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c14cb1ba-34f5-4a76-aa8e-20772cdcfe20", "f01f0f1b-e02c-41a7-bb1c-fc4b57e0f923", "Super Administrator", "SUPER ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42a99866-dcbf-44b9-a9d8-9bb5419d7a32", "460b1787-a74e-415c-b21c-12a03a278295", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42a99866-dcbf-44b9-a9d8-9bb5419d7a32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c14cb1ba-34f5-4a76-aa8e-20772cdcfe20");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c90484c-9d6a-415f-a5a7-a4a78ec4aa4c", "b0b0e730-7525-4675-a05d-53fba25fc664", "Super Administrator", "SUPER ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b767f674-3670-4bc9-b242-b5613f8efce6", "edce8336-0045-4159-b427-2ce0f5833c62", "Administrator", "ADMINISTRATOR" });
        }
    }
}
