using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Infrastructure.Migrations
{
    public partial class Persons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KnownAs",
                table: "persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "KnownAs",
                table: "persons");
        }
    }
}
