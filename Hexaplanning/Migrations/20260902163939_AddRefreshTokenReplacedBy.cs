using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexaplanning.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenReplacedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReplacedByToken",
                table: "RefreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "a771962e-49c8-4b15-864a-d96eb42d4b21");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "eb4f2c1d-c232-425a-acdd-de0b7a677d43");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 2, 16, 39, 38, 946, DateTimeKind.Utc).AddTicks(8827), new DateTime(2026, 9, 2, 16, 39, 38, 946, DateTimeKind.Utc).AddTicks(8830) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 2, 16, 39, 38, 946, DateTimeKind.Utc).AddTicks(8839), new DateTime(2026, 9, 2, 16, 39, 38, 946, DateTimeKind.Utc).AddTicks(8839) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 2, 16, 39, 38, 946, DateTimeKind.Utc).AddTicks(8844), new DateTime(2026, 9, 2, 16, 39, 38, 946, DateTimeKind.Utc).AddTicks(8844) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 9, 2, 16, 39, 38, 946, DateTimeKind.Utc).AddTicks(8842), new DateTime(2026, 9, 2, 16, 39, 38, 946, DateTimeKind.Utc).AddTicks(8842) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplacedByToken",
                table: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "044523c4-bca7-448d-9385-1f9337a6caaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "681889a7-636a-44c9-a683-86f6beefd49d");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 25, 0, 5, 27, 382, DateTimeKind.Utc).AddTicks(8853), new DateTime(2026, 8, 25, 0, 5, 27, 382, DateTimeKind.Utc).AddTicks(8858) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 25, 0, 5, 27, 382, DateTimeKind.Utc).AddTicks(8878), new DateTime(2026, 8, 25, 0, 5, 27, 382, DateTimeKind.Utc).AddTicks(8879) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 25, 0, 5, 27, 382, DateTimeKind.Utc).AddTicks(8913), new DateTime(2026, 8, 25, 0, 5, 27, 382, DateTimeKind.Utc).AddTicks(8914) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 25, 0, 5, 27, 382, DateTimeKind.Utc).AddTicks(8906), new DateTime(2026, 8, 25, 0, 5, 27, 382, DateTimeKind.Utc).AddTicks(8907) });
        }
    }
}
