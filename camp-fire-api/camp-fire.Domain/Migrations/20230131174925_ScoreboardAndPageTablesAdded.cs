using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace camp_fire.Domain.Migrations
{
    public partial class ScoreboardAndPageTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScoreboardId",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreboardId",
                table: "Pages");
        }
    }
}
