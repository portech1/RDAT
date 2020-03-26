using Microsoft.EntityFrameworkCore.Migrations;

namespace RDAT.Migrations
{
    public partial class CompanyZip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Companys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Companys");
        }
    }
}
