using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class isFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFavorite",
                table: "Drivers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isFavorite",
                table: "Companys",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFavorite",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "isFavorite",
                table: "Companys");
        }
    }
}
