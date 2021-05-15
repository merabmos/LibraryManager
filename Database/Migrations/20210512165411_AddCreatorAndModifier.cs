using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddCreatorAndModifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Sectors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Sectors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Borrow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Borrow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "BooksShelves_Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "BooksShelves_Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "BooksShelves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "BooksShelves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Books_Genres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Books_Genres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Books_Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Books_Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmployeeId",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifierEmployeeId",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "BooksShelves_Books");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "BooksShelves_Books");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "BooksShelves");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "BooksShelves");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Books_Genres");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Books_Genres");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Books_Authors");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Books_Authors");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatorEmployeeId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ModifierEmployeeId",
                table: "Authors");
        }
    }
}
