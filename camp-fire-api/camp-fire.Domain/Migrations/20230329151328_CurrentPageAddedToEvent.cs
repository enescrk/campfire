using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace camp_fire.Domain.Migrations
{
    public partial class CurrentPageAddedToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPageId",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentUserId",
                table: "Events",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPageId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CurrentUserId",
                table: "Events");
        }
    }
}
