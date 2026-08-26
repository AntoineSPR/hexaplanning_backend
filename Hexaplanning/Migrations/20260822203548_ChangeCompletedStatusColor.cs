using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexaplanning.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCompletedStatusColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#37007f", new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1887), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1887) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1885), new DateTime(2026, 8, 22, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(1885) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "7aef43da-7d19-4e0a-b36a-1e03ec9c510c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "469dfd82-57f7-4a85-a186-c63cb81585a2");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9114), new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9115) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9109), new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9109) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9099), new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9099) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9040), new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9043) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9060), new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#004da5", new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9066), new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9066) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("b34563d0-1ae5-42f9-950a-beffa4e27dce"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9063), new DateTime(2026, 8, 22, 19, 22, 45, 264, DateTimeKind.Utc).AddTicks(9063) });
        }
    }
}
