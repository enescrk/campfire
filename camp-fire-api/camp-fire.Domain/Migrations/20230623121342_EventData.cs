using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace camp_fire.Domain.Migrations
{
    public partial class EventData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scoreboards_Pages_PageId",
                table: "Scoreboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Scoreboards_Users_UserId",
                table: "Scoreboards");

            migrationBuilder.DropIndex(
                name: "IX_Scoreboards_PageId",
                table: "Scoreboards");

            migrationBuilder.DropIndex(
                name: "IX_Scoreboards_UserId",
                table: "Scoreboards");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Stories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoreboardId1",
                table: "Pages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventData",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ScoreboardId1",
                table: "Pages",
                column: "ScoreboardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Scoreboards_ScoreboardId1",
                table: "Pages",
                column: "ScoreboardId1",
                principalTable: "Scoreboards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Scoreboards_ScoreboardId1",
                table: "Pages");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Pages_ScoreboardId1",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "ScoreboardId1",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "EventData",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Events");

            migrationBuilder.CreateIndex(
                name: "IX_Scoreboards_PageId",
                table: "Scoreboards",
                column: "PageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scoreboards_UserId",
                table: "Scoreboards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scoreboards_Pages_PageId",
                table: "Scoreboards",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scoreboards_Users_UserId",
                table: "Scoreboards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
