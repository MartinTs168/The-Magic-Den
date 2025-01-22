using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGamesShop.Infrastructure.Migrations
{
    public partial class AddNewFieldsToGame : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPlayers",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
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
