using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class AddOldCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldCompanyId",
                table: "Drivers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldCompanyId",
                table: "Drivers");
        }
    }
}
