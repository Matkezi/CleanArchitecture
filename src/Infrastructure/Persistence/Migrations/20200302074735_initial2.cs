using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenceNumber",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SkipperInsurancePolicyUrl",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "SkipperId",
                table: "Licences",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIssue",
                table: "Licences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsoCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkipperLanguage",
                columns: table => new
                {
                    SkipperId = table.Column<string>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkipperLanguage", x => new { x.SkipperId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SkipperLanguage_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
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
                name: "FK_Licences_AspNetUsers_SkipperId",
                table: "Licences",
                column: "SkipperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licences_AspNetUsers_SkipperId",
                table: "Licences");

            migrationBuilder.DropTable(
                name: "SkipperLanguage");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Licences_SkipperId",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "SkipperId",
                table: "Licences",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicenceNumber",
                table: "Licences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkipperInsurancePolicyUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
