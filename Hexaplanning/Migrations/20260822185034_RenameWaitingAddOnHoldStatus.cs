using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexaplanning.Migrations
{
    /// <inheritdoc />
    public partial class RenameWaitingAddOnHoldStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "fa7a7cee-0d8c-4d82-aaf9-4220f14c8b5f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "eabd3aee-38b2-4e89-8877-f83cbfc78b49");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9855), new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9855) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9853), new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9853) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9848), new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9849) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "Color", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "#9E9E9E", new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9742), "À accomplir", new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9745) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#B87FED", new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9812), new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9812) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#4CAF7D", new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9818), new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9819) });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Color", "CreatedAt", "Icon", "IsArchived", "Name", "UpdatedAt" },
                values: new object[] { new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"), "#E2A72B", new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9816), null, false, "En attente", new DateTime(2026, 8, 22, 18, 50, 33, 875, DateTimeKind.Utc).AddTicks(9816) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "e8321a53-0189-482e-a4a6-f13222be1092");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "519f4004-51fa-4ce3-a914-11b1b0a212bf");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(564), new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(564) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(562), new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(562) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(558), new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(558) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "Color", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { "#FFA500", new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(517), "En attente", new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(519) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#FBA500", new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(528), new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(528) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#FFF500", new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(530), new DateTime(2026, 8, 21, 20, 56, 53, 943, DateTimeKind.Utc).AddTicks(530) });
        }
    }
}
