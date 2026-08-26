using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexaplanning.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuestGroupId",
                table: "Quests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "f9b237b5-13be-410f-a3f3-4e15c138656b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "f1d54b8c-f29f-4bba-8855-39c218b67fb6");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6539), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6536), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6536) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6522), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6523) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6478), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6489), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6489) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6495), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6495) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6492), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6492) });

            migrationBuilder.CreateIndex(
                name: "IX_Quests_QuestGroupId",
                table: "Quests",
                column: "QuestGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestGroups_UserId",
                table: "QuestGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_QuestGroups_QuestGroupId",
                table: "Quests",
                column: "QuestGroupId",
                principalTable: "QuestGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_QuestGroups_QuestGroupId",
                table: "Quests");

            migrationBuilder.DropTable(
                name: "QuestGroups");

            migrationBuilder.DropIndex(
                name: "IX_Quests_QuestGroupId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "QuestGroupId",
                table: "Quests");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "9a71f40c-7454-4107-8fc9-88a8ffee7e03");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "a67b9e94-ff61-41f7-a618-e0b9a643148e");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1916), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1916) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1913), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1913) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1909), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1871), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1873) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1882), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1883) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1887), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1887) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1885), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1885) });
        }
    }
}
