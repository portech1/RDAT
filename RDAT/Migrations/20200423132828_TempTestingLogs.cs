using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class TempTestingLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreateBatch",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivePool = table.Column<int>(nullable: false),
                    DrugPercentage = table.Column<int>(nullable: false),
                    DrugTestDate = table.Column<DateTime>(nullable: false),
                    AlcoholPercentage = table.Column<int>(nullable: false),
                    AlcoholTestDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateBatch", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TempTestingLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Selectiondatedrug = table.Column<DateTime>(nullable: true),
                    Selectiondatealcohol = table.Column<DateTime>(nullable: true),
                    SSN = table.Column<string>(nullable: true),
                    Driver_Id = table.Column<int>(nullable: false),
                    Test_Type = table.Column<string>(nullable: true),
                    Drug_Percentage = table.Column<double>(nullable: false),
                    Alcohol_Percentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempTestingLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateBatch");

            migrationBuilder.DropTable(
                name: "TempTestingLogs");
        }
    }
}
