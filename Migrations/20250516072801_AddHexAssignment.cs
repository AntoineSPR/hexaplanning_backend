using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class AddHexAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HexAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestId = table.Column<string>(type: "text", nullable: true),
                    Q = table.Column<int>(type: "integer", nullable: false),
                    R = table.Column<int>(type: "integer", nullable: false),
                    S = table.Column<int>(type: "integer", nullable: false),
                    QuestId1 = table.Column<Guid>(type: "uuid", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HexAssignment");
        }
    }
}
