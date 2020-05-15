﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class GuestTos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GuestTOS",
                table: "Bookings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestTOS",
                table: "Bookings");
        }
    }
}
