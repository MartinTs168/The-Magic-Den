using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGamesShop.Infrastructure.Migrations
{
    public partial class AddNewFieldsToGamesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgeRating",
                table: "Games",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumberOfPlayers",
                table: "Games",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRating",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "NumberOfPlayers",
                table: "Games");
        }
    }
}
