using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorFatura.Migrations
{
    public partial class AddPdfFieldsToFaturas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "Faturas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "OriginalFileNameBytes",
                table: "Faturas",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "Faturas");

            migrationBuilder.DropColumn(
                name: "OriginalFileNameBytes",
                table: "Faturas");
        }
    }
}
