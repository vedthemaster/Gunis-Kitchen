using Microsoft.EntityFrameworkCore.Migrations;

namespace Gunis.Kitchen.Migrations
{
    public partial class AddingAdminKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminKey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminKey",
                table: "AspNetUsers");
        }
    }
}
