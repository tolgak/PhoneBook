using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Repository.Migrations
{
    public partial class response_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cntPeopleNearBy",
                table: "ReportRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cntPhonesNearBy",
                table: "ReportRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cntPeopleNearBy",
                table: "ReportRequests");

            migrationBuilder.DropColumn(
                name: "cntPhonesNearBy",
                table: "ReportRequests");
        }
    }
}
