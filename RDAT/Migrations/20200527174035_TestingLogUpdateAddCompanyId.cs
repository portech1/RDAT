using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class TestingLogUpdateAddCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Company_Id",
                table: "TestingLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RandomPool",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Random_Test_Selection_Date = table.Column<DateTime>(nullable: false),
                    Batch_Number = table.Column<int>(nullable: false),
                    Active_Enrolled_Drivers = table.Column<int>(nullable: false),
                    Selected_Drivers = table.Column<int>(nullable: false),
                    Excused_Drivers = table.Column<int>(nullable: false),
                    Positive_Tested_Drivers = table.Column<int>(nullable: false),
                    Negative_Tested_Drivers = table.Column<int>(nullable: false),
                    Selection_Test_Ratio = table.Column<double>(nullable: false),
                    Annual_Ratio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomPool", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RandomPool");

            migrationBuilder.DropColumn(
                name: "Company_Id",
                table: "TestingLogs");
        }
    }
}
