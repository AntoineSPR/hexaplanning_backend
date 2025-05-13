using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class Complete_Quest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Quests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRepeatable",
                table: "Quests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Quests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "IsRepeatable",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Quests");
        }
    }
}
