using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class ConvertFirstNameLastNameToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the new Name column
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            // Migrate existing data: Combine FirstName and LastName into Name
            migrationBuilder.Sql(
                @"UPDATE ""AspNetUsers"" 
                  SET ""Name"" = CONCAT(""FirstName"", ' ', ""LastName"") 
                  WHERE ""FirstName"" IS NOT NULL AND ""LastName"" IS NOT NULL");

            // For users without FirstName or LastName, use a default
            migrationBuilder.Sql(
                @"UPDATE ""AspNetUsers"" 
                  SET ""Name"" = COALESCE(""FirstName"", COALESCE(""LastName"", ""UserName"")) 
                  WHERE ""Name"" = ''");

            // Drop the old columns
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "6a9c53c9-92e0-485d-b65b-335e8a7d0b30");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "d9f5078e-ba62-4398-a3d7-505c6ed90333");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2363), new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2363) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2357), new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2358) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2346), new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2347) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2193), new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2198) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2235), new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2236) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2241), new DateTime(2025, 11, 18, 11, 8, 11, 98, DateTimeKind.Utc).AddTicks(2241) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add back the FirstName and LastName columns
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            // Attempt to split Name back into FirstName and LastName
            migrationBuilder.Sql(
                @"UPDATE ""AspNetUsers"" 
                  SET ""FirstName"" = SPLIT_PART(""Name"", ' ', 1),
                      ""LastName"" = CASE 
                          WHEN POSITION(' ' IN ""Name"") > 0 
                          THEN SUBSTRING(""Name"" FROM POSITION(' ' IN ""Name"") + 1)
                          ELSE ''
                      END");

            // Drop the Name column
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "37d9aaba-aa9b-4f00-8fa5-3c83c3549fe2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "9cb95139-bb69-4824-8d0a-f66ec3afef91");

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3828), new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3828) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3825), new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3825) });

            migrationBuilder.UpdateData(
                table: "Priorities",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3821), new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3821) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("17c07323-d5b4-4568-b773-de3487ff30b1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3779), new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3781) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("2281c955-b3e1-49dc-be62-6a7912bb46b3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3798), new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3798) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: new Guid("6662dfc1-9c40-4d78-806f-34cd22e07023"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3800), new DateTime(2025, 11, 18, 11, 3, 16, 316, DateTimeKind.Utc).AddTicks(3800) });
        }
    }
}
