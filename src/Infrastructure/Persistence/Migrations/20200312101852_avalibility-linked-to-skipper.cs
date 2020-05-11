using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class avalibilitylinkedtoskipper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SkipperId1",
                table: "Availabilities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_SkipperId1",
                table: "Availabilities",
                column: "SkipperId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_AspNetUsers_SkipperId1",
                table: "Availabilities",
                column: "SkipperId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_AspNetUsers_SkipperId1",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_SkipperId1",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "SkipperId1",
                table: "Availabilities");
        }
    }
}
