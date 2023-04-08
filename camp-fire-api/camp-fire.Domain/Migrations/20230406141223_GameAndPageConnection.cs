using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace camp_fire.Domain.Migrations
{
    public partial class GameAndPageConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_GameId",
                table: "Pages",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Games_GameId",
                table: "Pages",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Games_GameId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_GameId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Pages");
        }
    }
}
