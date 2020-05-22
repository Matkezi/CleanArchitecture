using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class AddedIsActiveFlagToBoats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Boats",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Boats");
        }
    }
}
