using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class CompanyModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Companys",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Companys",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Companys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Companys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OldID",
                table: "Companys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Companys",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "Companys",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "OldID",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Companys");
        }
    }
}
