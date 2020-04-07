using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class TestingLogChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_Id",
                table: "TestingLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Test_Type",
                table: "TestingLogs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Alcohol_Percentage",
                table: "TestingLogs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Batch_Id",
                table: "TestingLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "TestingLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Drug_Percentage",
                table: "TestingLogs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Reported_Results",
                table: "TestingLogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResultsDate",
                table: "TestingLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Selectiondatealcohol",
                table: "TestingLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Selectiondatedrug",
                table: "TestingLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Specimen_Id",
                table: "TestingLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Test_Process_Id",
                table: "TestingLogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alcohol_Percentage",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "Batch_Id",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "Drug_Percentage",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "Reported_Results",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "ResultsDate",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "Selectiondatealcohol",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "Selectiondatedrug",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "Specimen_Id",
                table: "TestingLogs");

            migrationBuilder.DropColumn(
                name: "Test_Process_Id",
                table: "TestingLogs");

            migrationBuilder.AlterColumn<int>(
                name: "Test_Type",
                table: "TestingLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Company_Id",
                table: "TestingLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
