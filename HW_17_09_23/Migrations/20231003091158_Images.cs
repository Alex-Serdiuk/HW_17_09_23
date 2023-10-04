using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_17_09_23.Migrations
{
    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "SkillNames",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "AboutMes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillNames_ImageId",
                table: "SkillNames",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AboutMes_ImageId",
                table: "AboutMes",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutMes_Images_ImageId",
                table: "AboutMes",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillNames_Images_ImageId",
                table: "SkillNames",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutMes_Images_ImageId",
                table: "AboutMes");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillNames_Images_ImageId",
                table: "SkillNames");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_SkillNames_ImageId",
                table: "SkillNames");

            migrationBuilder.DropIndex(
                name: "IX_AboutMes_ImageId",
                table: "AboutMes");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "SkillNames");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AboutMes");
        }
    }
}
