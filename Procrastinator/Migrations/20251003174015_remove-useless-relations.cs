using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procrastinator.Migrations
{
    /// <inheritdoc />
    public partial class removeuselessrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexAssignments_AspNetUsers_UserId",
                table: "HexAssignments");

            migrationBuilder.DropIndex(
                name: "IX_HexAssignments_Q_R_S_UserId",
                table: "HexAssignments");

            migrationBuilder.DropIndex(
                name: "IX_HexAssignments_UserId",
                table: "HexAssignments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HexAssignments");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "HexAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                column: "ConcurrencyStamp",
                value: "6e6b1499-95a0-4a50-805a-ad34fc235cbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                column: "ConcurrencyStamp",
                value: "d964d09a-9884-4857-8074-e1bcc1eda588");

            migrationBuilder.CreateIndex(
                name: "IX_HexAssignments_Q_R_S_UserId",
                table: "HexAssignments",
                columns: new[] { "Q", "R", "S", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HexAssignments_UserId",
                table: "HexAssignments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HexAssignments_AspNetUsers_UserId",
                table: "HexAssignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
