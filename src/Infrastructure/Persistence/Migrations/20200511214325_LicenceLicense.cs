using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class LicenceLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedSkippers_AspNetUsers_CharterID",
                table: "TrustedSkippers");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustedSkippers_AspNetUsers_SkipperID",
                table: "TrustedSkippers");

            migrationBuilder.DropForeignKey(
                name: "FK_UnTrustedSkippers_AspNetUsers_CharterID",
                table: "UnTrustedSkippers");

            migrationBuilder.DropForeignKey(
                name: "FK_UnTrustedSkippers_AspNetUsers_SkipperID",
                table: "UnTrustedSkippers");

            migrationBuilder.DropTable(
                name: "Licences");

            migrationBuilder.DropTable(
                name: "SkipperLanguage");

            migrationBuilder.DropColumn(
                name: "GuestMessege",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BoathPhotoUrl",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "MinimalRequiredLicence",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "YearOfFirstLicence",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CharterID",
                table: "UnTrustedSkippers",
                newName: "CharterId");

            migrationBuilder.RenameColumn(
                name: "SkipperID",
                table: "UnTrustedSkippers",
                newName: "SkipperId");

            migrationBuilder.RenameIndex(
                name: "IX_UnTrustedSkippers_CharterID",
                table: "UnTrustedSkippers",
                newName: "IX_UnTrustedSkippers_CharterId");

            migrationBuilder.RenameColumn(
                name: "CharterID",
                table: "TrustedSkippers",
                newName: "CharterId");

            migrationBuilder.RenameColumn(
                name: "SkipperID",
                table: "TrustedSkippers",
                newName: "SkipperId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedSkippers_CharterID",
                table: "TrustedSkippers",
                newName: "IX_TrustedSkippers_CharterId");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "SkipperPreRegistration",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "OnboardingLocation",
                table: "Bookings",
                newName: "OnBoardingLocation");

            migrationBuilder.RenameColumn(
                name: "GuestTOS",
                table: "Bookings",
                newName: "GuestTos");

            migrationBuilder.RenameColumn(
                name: "BookingURL",
                table: "Bookings",
                newName: "BookingUrl");

            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "BookingHistories",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "bookingRejected",
                table: "BookingHistories",
                newName: "BookingRejected");

            migrationBuilder.RenameColumn(
                name: "OIB",
                table: "AspNetUsers",
                newName: "Oib");

            migrationBuilder.AddColumn<string>(
                name: "GuestMessage",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BoatPhotoUrl",
                table: "Boats",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimalRequiredLicense",
                table: "Boats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearOfFirstLicense",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidTo = table.Column<DateTime>(nullable: false),
                    DateOfIssue = table.Column<DateTime>(nullable: false),
                    LicenseType = table.Column<int>(nullable: false),
                    LicenseUrl = table.Column<string>(nullable: true),
                    SkipperId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licenses_AspNetUsers_SkipperId",
                        column: x => x.SkipperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkipperLanguages",
                columns: table => new
                {
                    SkipperId = table.Column<string>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    LevelOfKnowledge = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkipperLanguages", x => new { x.SkipperId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SkipperLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkipperLanguages_AspNetUsers_SkipperId",
                        column: x => x.SkipperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_SkipperId",
                table: "Licenses",
                column: "SkipperId",
                unique: true,
                filter: "[SkipperId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SkipperLanguages_LanguageId",
                table: "SkipperLanguages",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedSkippers_AspNetUsers_CharterId",
                table: "TrustedSkippers",
                column: "CharterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedSkippers_AspNetUsers_SkipperId",
                table: "TrustedSkippers",
                column: "SkipperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnTrustedSkippers_AspNetUsers_CharterId",
                table: "UnTrustedSkippers",
                column: "CharterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnTrustedSkippers_AspNetUsers_SkipperId",
                table: "UnTrustedSkippers",
                column: "SkipperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedSkippers_AspNetUsers_CharterId",
                table: "TrustedSkippers");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustedSkippers_AspNetUsers_SkipperId",
                table: "TrustedSkippers");

            migrationBuilder.DropForeignKey(
                name: "FK_UnTrustedSkippers_AspNetUsers_CharterId",
                table: "UnTrustedSkippers");

            migrationBuilder.DropForeignKey(
                name: "FK_UnTrustedSkippers_AspNetUsers_SkipperId",
                table: "UnTrustedSkippers");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "SkipperLanguages");

            migrationBuilder.DropColumn(
                name: "GuestMessage",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BoatPhotoUrl",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "MinimalRequiredLicense",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "YearOfFirstLicense",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CharterId",
                table: "UnTrustedSkippers",
                newName: "CharterID");

            migrationBuilder.RenameColumn(
                name: "SkipperId",
                table: "UnTrustedSkippers",
                newName: "SkipperID");

            migrationBuilder.RenameIndex(
                name: "IX_UnTrustedSkippers_CharterId",
                table: "UnTrustedSkippers",
                newName: "IX_UnTrustedSkippers_CharterID");

            migrationBuilder.RenameColumn(
                name: "CharterId",
                table: "TrustedSkippers",
                newName: "CharterID");

            migrationBuilder.RenameColumn(
                name: "SkipperId",
                table: "TrustedSkippers",
                newName: "SkipperID");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedSkippers_CharterId",
                table: "TrustedSkippers",
                newName: "IX_TrustedSkippers_CharterID");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "SkipperPreRegistration",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "OnBoardingLocation",
                table: "Bookings",
                newName: "OnboardingLocation");

            migrationBuilder.RenameColumn(
                name: "GuestTos",
                table: "Bookings",
                newName: "GuestTOS");

            migrationBuilder.RenameColumn(
                name: "BookingUrl",
                table: "Bookings",
                newName: "BookingURL");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "BookingHistories",
                newName: "dateTime");

            migrationBuilder.RenameColumn(
                name: "BookingRejected",
                table: "BookingHistories",
                newName: "bookingRejected");

            migrationBuilder.RenameColumn(
                name: "Oib",
                table: "AspNetUsers",
                newName: "OIB");

            migrationBuilder.AddColumn<string>(
                name: "GuestMessege",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BoathPhotoUrl",
                table: "Boats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimalRequiredLicence",
                table: "Boats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearOfFirstLicence",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Licences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LicenceType = table.Column<int>(type: "int", nullable: false),
                    LicenceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkipperId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licences_AspNetUsers_SkipperId",
                        column: x => x.SkipperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkipperLanguage",
                columns: table => new
                {
                    SkipperId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    LevelOfKnowledge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkipperLanguage", x => new { x.SkipperId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SkipperLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkipperLanguage_AspNetUsers_SkipperId",
                        column: x => x.SkipperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licences_SkipperId",
                table: "Licences",
                column: "SkipperId",
                unique: true,
                filter: "[SkipperId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SkipperLanguage_LanguageId",
                table: "SkipperLanguage",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedSkippers_AspNetUsers_CharterID",
                table: "TrustedSkippers",
                column: "CharterID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedSkippers_AspNetUsers_SkipperID",
                table: "TrustedSkippers",
                column: "SkipperID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnTrustedSkippers_AspNetUsers_CharterID",
                table: "UnTrustedSkippers",
                column: "CharterID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnTrustedSkippers_AspNetUsers_SkipperID",
                table: "UnTrustedSkippers",
                column: "SkipperID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
