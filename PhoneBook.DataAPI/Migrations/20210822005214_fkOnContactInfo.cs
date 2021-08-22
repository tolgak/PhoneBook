using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Repository.Migrations
{
    public partial class fkOnContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_PersonId",
                table: "ContactInfos",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfos_People_PersonId",
                table: "ContactInfos",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfos_People_PersonId",
                table: "ContactInfos");

            migrationBuilder.DropIndex(
                name: "IX_ContactInfos_PersonId",
                table: "ContactInfos");
        }
    }
}
