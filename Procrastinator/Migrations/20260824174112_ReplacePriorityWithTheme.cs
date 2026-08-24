using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class ReplacePriorityWithTheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Priorities_PriorityId",
                table: "Quests");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropIndex(
                name: "IX_Quests_PriorityId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Quests");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryTheme",
                table: "Quests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ThemeId",
                table: "Quests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Themes_AspNetUsers_UserId",
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
                value: "75f3f741-17c3-4470-a3d8-d3780ef90994");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "627bcc34-dbec-484b-bdf0-5b74f8f80829");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 17, 41, 11, 628, DateTimeKind.Utc).AddTicks(6169), new DateTime(2026, 8, 24, 17, 41, 11, 628, DateTimeKind.Utc).AddTicks(6173) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 17, 41, 11, 628, DateTimeKind.Utc).AddTicks(6185), new DateTime(2026, 8, 24, 17, 41, 11, 628, DateTimeKind.Utc).AddTicks(6185) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 17, 41, 11, 628, DateTimeKind.Utc).AddTicks(6198), new DateTime(2026, 8, 24, 17, 41, 11, 628, DateTimeKind.Utc).AddTicks(6198) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 24, 17, 41, 11, 628, DateTimeKind.Utc).AddTicks(6187), new DateTime(2026, 8, 24, 17, 41, 11, 628, DateTimeKind.Utc).AddTicks(6188) });

            migrationBuilder.CreateIndex(
                name: "IX_Quests_ThemeId",
                table: "Quests",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_UserId",
                table: "Themes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Themes_ThemeId",
                table: "Quests",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Themes_ThemeId",
                table: "Quests");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Quests_ThemeId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "IsPrimaryTheme",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Quests");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Statuses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PriorityId",
                table: "Quests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BorderColor = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
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

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "BorderColor", "Color", "CreatedAt", "Icon", "IsArchived", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"), null, "#797676", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6539), "tertiary", false, "Quête tertiaire", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6540) },
                    { new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"), "#D3D3D3", "#8A2BE2", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6536), "secondary", false, "Quête secondaire", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6536) },
                    { new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"), "#E28A2B", "#E28A2B", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6522), "primary", false, "Quête principale", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6523) }
                });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#9E9E9E", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6478), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#B87FED", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6489), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6489) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#37007f", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6495), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6495) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#ff9500", new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6492), new DateTime(2026, 8, 24, 0, 12, 56, 290, DateTimeKind.Utc).AddTicks(6492) });

            migrationBuilder.CreateIndex(
                name: "IX_Quests_PriorityId",
                table: "Quests",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Priorities_PriorityId",
                table: "Quests",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
