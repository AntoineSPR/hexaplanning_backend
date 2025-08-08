using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class CompleteQuestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HexAssignments_QuestId",
                table: "HexAssignments");

            migrationBuilder.AddColumn<int>(
                name: "HexAssignmentId",
                table: "Quests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAssigned",
                table: "Quests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Quests_HexAssignmentId",
                table: "Quests",
                column: "HexAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HexAssignments_QuestId",
                table: "HexAssignments",
                column: "QuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_HexAssignments_HexAssignmentId",
                table: "Quests",
                column: "HexAssignmentId",
                principalTable: "HexAssignments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_HexAssignments_HexAssignmentId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_HexAssignmentId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_HexAssignments_QuestId",
                table: "HexAssignments");

            migrationBuilder.DropColumn(
                name: "HexAssignmentId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "IsAssigned",
                table: "Quests");

            migrationBuilder.CreateIndex(
                name: "IX_HexAssignments_QuestId",
                table: "HexAssignments",
                column: "QuestId",
                unique: true);
        }
    }
}
