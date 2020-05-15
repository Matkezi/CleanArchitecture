using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class AddedGuestNationalityFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestNationality",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "GuestNationalityId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_GuestNationalityId",
                table: "Bookings",
                column: "GuestNationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Countries_GuestNationalityId",
                table: "Bookings",
                column: "GuestNationalityId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Countries_GuestNationalityId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_GuestNationalityId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "GuestNationalityId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "GuestNationality",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
