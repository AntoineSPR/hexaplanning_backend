using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class seedprio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "42c9926e-ced4-462a-a18b-47e9646282c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "33cff182-23da-4779-8d1e-fd9b6939b324");

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "Color", "CreatedAt", "Icon", "IsArchived", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"), "#FFF500", new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8832), null, false, "Quête tertiaire", new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8832) },
                    { new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"), "#FBA500", new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8830), null, false, "Quête secondaire", new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8830) },
                    { new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"), "#FFA500", new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8825), null, false, "Quête principale", new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8825) }
                });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8762), new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8765) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8773), new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8773) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8776), new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8776) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"));

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"));

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "3699611e-fb73-4df4-9eea-a33556843296");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "ab7993f7-245e-4c02-babe-a710947077e7");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5789), new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5792) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5799), new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5799) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5802), new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5803) });
        }
    }
}
