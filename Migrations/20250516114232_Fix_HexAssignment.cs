using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class Fix_HexAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HexAssignment");

            migrationBuilder.CreateTable(
                name: "HexAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Q = table.Column<int>(type: "integer", nullable: false),
                    R = table.Column<int>(type: "integer", nullable: false),
                    S = table.Column<int>(type: "integer", nullable: false),
                    QuestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HexAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HexAssignments_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HexAssignments_QuestId",
                table: "HexAssignments",
                column: "QuestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HexAssignments");

            migrationBuilder.CreateTable(
                name: "HexAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    Q = table.Column<int>(type: "integer", nullable: false),
                    QuestId = table.Column<string>(type: "text", nullable: true),
                    R = table.Column<int>(type: "integer", nullable: false),
                    S = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HexAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HexAssignment_Quests_QuestId1",
                        column: x => x.QuestId1,
                        principalTable: "Quests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HexAssignment_QuestId1",
                table: "HexAssignment",
                column: "QuestId1");
        }
    }
}
