using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class AddedSkipperRequestTimeToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SkipperRequestTime",
                table: "Bookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkipperRequestTime",
                table: "Bookings");
        }
    }
}
