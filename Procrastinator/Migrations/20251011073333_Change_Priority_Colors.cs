using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class Change_Priority_Colors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "bf2e30c7-9c15-4051-a027-d4f36721760b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "2db5978b-c294-43c6-957f-6c28e46bd991");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#797676", new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5531), new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5531) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#8A2BE2", new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5513), new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5513) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#E28A2B", new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5504), new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5504) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5407), new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5413) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5431), new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5432) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5435), new DateTime(2025, 10, 11, 7, 33, 32, 918, DateTimeKind.Utc).AddTicks(5436) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "61df1ed9-ffe6-4d9e-9a8b-57b89a065d53");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "359a6ba5-b292-4810-851f-6bf18bd359e2");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#FFF500", new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(7029), new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(7029) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#FBA500", new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(7014), new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(7014) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "Color", "CreatedAt", "UpdatedAt" },
                values: new object[] { "#FFA500", new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(7006), new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(7006) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(6935), new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(6938) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(6949), new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(6949) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(6954), new DateTime(2025, 10, 10, 19, 40, 10, 460, DateTimeKind.Utc).AddTicks(6954) });
        }
    }
}
