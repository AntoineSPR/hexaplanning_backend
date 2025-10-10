using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class newfieldbordercolorpriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BorderColor",
                table: "Priorities",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "61b47fee-8ba2-4d27-911e-3a214c92680f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "0291b5f3-2e56-49c5-9ec4-0dd63a771292");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "BorderColor", "CreatedAt", "Icon", "UpdatedAt" },
                values: new object[] { null, new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5859), "tertiary", new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5859) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "BorderColor", "CreatedAt", "Icon", "UpdatedAt" },
                values: new object[] { null, new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5813), "secondary", new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5814) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "BorderColor", "CreatedAt", "Icon", "UpdatedAt" },
                values: new object[] { null, new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5809), "primary", new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5809) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5741), new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5744) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5764), new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5764) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5767), new DateTime(2025, 10, 9, 19, 3, 22, 865, DateTimeKind.Utc).AddTicks(5767) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorderColor",
                table: "Priorities");

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

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "Icon", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8832), null, new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8832) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "Icon", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8830), null, new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8830) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "Icon", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8825), null, new DateTime(2025, 10, 3, 17, 56, 31, 321, DateTimeKind.Utc).AddTicks(8825) });

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
    }
}
