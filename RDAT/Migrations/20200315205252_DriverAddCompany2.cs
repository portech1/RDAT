using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class DriverAddCompany2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_d",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "Company_id",
                table: "Drivers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_id",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "Company_d",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
