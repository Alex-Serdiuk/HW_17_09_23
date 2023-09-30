using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_17_09_23.Migrations
{
    public partial class Unit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "SkillNameId",
                table: "Skills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SkillNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillNames", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillNameId",
                table: "Skills",
                column: "SkillNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_SkillNames_SkillNameId",
                table: "Skills",
                column: "SkillNameId",
                principalTable: "SkillNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_SkillNames_SkillNameId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "SkillNames");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SkillNameId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SkillNameId",
                table: "Skills");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Skills",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
