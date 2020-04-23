using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class TempTestingLogsUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alcohol_Percentage",
                table: "TempTestingLogs");

            migrationBuilder.DropColumn(
                name: "Drug_Percentage",
                table: "TempTestingLogs");

            migrationBuilder.DropColumn(
                name: "Selectiondatealcohol",
                table: "TempTestingLogs");

            migrationBuilder.DropColumn(
                name: "Selectiondatedrug",
                table: "TempTestingLogs");

            migrationBuilder.AddColumn<int>(
                name: "Company_Id",
                table: "TempTestingLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TempTestingLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver_Name",
                table: "TempTestingLogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TempTestingLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TempTestingLogs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                table: "TempTestingLogs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_Id",
                table: "TempTestingLogs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TempTestingLogs");

            migrationBuilder.DropColumn(
                name: "Driver_Name",
                table: "TempTestingLogs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TempTestingLogs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TempTestingLogs");

            migrationBuilder.DropColumn(
                name: "is_delete",
                table: "TempTestingLogs");

            migrationBuilder.AddColumn<double>(
                name: "Alcohol_Percentage",
                table: "TempTestingLogs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Drug_Percentage",
                table: "TempTestingLogs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Selectiondatealcohol",
                table: "TempTestingLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Selectiondatedrug",
                table: "TempTestingLogs",
                type: "datetime2",
                nullable: true);
        }
    }
}
