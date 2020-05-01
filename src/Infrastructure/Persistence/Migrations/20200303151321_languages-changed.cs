using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class languageschanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsoCode",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Language");

            migrationBuilder.AddColumn<string>(
                name: "CountryNameEnglish",
                table: "Language",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryNameNative",
                table: "Language",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageNameEnglish",
                table: "Language",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageNameNative",
                table: "Language",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoLetterISOLanguageName",
                table: "Language",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryNameEnglish",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "CountryNameNative",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "LanguageNameEnglish",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "LanguageNameNative",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "TwoLetterISOLanguageName",
                table: "Language");

            migrationBuilder.AddColumn<string>(
                name: "IsoCode",
                table: "Language",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Language",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
