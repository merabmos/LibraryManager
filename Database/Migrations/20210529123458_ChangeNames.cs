using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ChangeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Sectors",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Sectors",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Sections",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Sections",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Genres",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Genres",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Customers",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Customers",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Borrow",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Borrow",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "BooksShelves_Books",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "BooksShelves_Books",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "BooksShelves",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "BooksShelves",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Books_Genres",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Books_Genres",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Books_Authors",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Books_Authors",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Books",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Books",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "ModifierEmployeeId",
                table: "Authors",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "CreatorEmployeeId",
                table: "Authors",
                newName: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Sectors",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Sectors",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Sections",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Sections",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Genres",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Genres",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Customers",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Customers",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Borrow",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Borrow",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "BooksShelves_Books",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "BooksShelves_Books",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "BooksShelves",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "BooksShelves",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Books_Genres",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Books_Genres",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Books_Authors",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Books_Authors",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Books",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Books",
                newName: "CreatorEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Authors",
                newName: "ModifierEmployeeId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Authors",
                newName: "CreatorEmployeeId");
        }
    }
}
