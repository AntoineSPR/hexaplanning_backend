using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class seedStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Color", "CreatedAt", "Icon", "IsArchived", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"), "#FFA500", new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5789), null, false, "En attente", new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5792) },
                    { new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"), "#FBA500", new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5799), null, false, "En cours", new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5799) },
                    { new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"), "#FFF500", new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5802), null, false, "Terminée", new DateTime(2025, 10, 3, 17, 52, 3, 480, DateTimeKind.Utc).AddTicks(5803) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"));

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "d1579744-e9b5-44d2-87e7-07952b000093");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "4b976125-3e77-490a-bfa2-d5b3746eba00");
        }
    }
}
