using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class TestingLogChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Driver_Name",
                table: "TestingLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Driver_Name",
                table: "TestingLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
