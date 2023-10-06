using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_17_09_23.Migrations
{
    public partial class Profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AboutMes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "AboutMes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AboutMes",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "AboutMes");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "AboutMes");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AboutMes");
        }
    }
}
