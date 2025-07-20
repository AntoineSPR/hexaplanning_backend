using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class CleanQuests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apprehension",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "ExperienceGain",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "IsRepeatable",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Quests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Apprehension",
                table: "Quests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Quests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Quests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceGain",
                table: "Quests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRepeatable",
                table: "Quests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Quests",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
