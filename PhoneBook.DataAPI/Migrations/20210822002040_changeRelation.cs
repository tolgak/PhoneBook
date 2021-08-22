using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Repository.Migrations
{
    public partial class changeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                table: "People");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "ContactInfos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ContactInfos");

            migrationBuilder.AddColumn<string>(
                name: "ContactInfoId",
                table: "People",
                type: "text",
                nullable: true);
        }
    }
}
