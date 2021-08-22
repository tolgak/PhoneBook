using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.ReportHandler.Migrations
{
    public partial class initialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RequestLocation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportRequests");
        }
    }
}
