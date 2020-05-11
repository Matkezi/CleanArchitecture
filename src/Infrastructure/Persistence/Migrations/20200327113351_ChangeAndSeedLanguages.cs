using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class ChangeAndSeedLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "CountryNameEnglish",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "CountryNameNative",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "LanguageNameEnglish",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "LanguageNameNative",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "TwoLetterISOLanguageName",
                table: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoLetterCode",
                table: "Languages",
                nullable: true);            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "TwoLetterCode",
                table: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "CountryNameEnglish",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryNameNative",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageNameEnglish",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageNameNative",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoLetterISOLanguageName",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
