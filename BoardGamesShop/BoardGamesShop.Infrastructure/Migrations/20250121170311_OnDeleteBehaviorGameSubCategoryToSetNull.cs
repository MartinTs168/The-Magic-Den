using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGamesShop.Infrastructure.Migrations
{
    public partial class OnDeleteBehaviorGameSubCategoryToSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_SubCategories_SubCategoryId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_Name",
                table: "SubCategories",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_SubCategories_SubCategoryId",
                table: "Games",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_SubCategories_SubCategoryId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_Name",
                table: "SubCategories");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_SubCategories_SubCategoryId",
                table: "Games",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
