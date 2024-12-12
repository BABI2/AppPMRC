using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppPMRC.Migrations
{
    /// <inheritdoc />
    public partial class allPul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 26, 42, 795, DateTimeKind.Utc).AddTicks(1906));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 26, 42, 795, DateTimeKind.Utc).AddTicks(2612));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 26, 42, 795, DateTimeKind.Utc).AddTicks(2614));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 26, 42, 795, DateTimeKind.Utc).AddTicks(2616));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 26, 42, 795, DateTimeKind.Utc).AddTicks(2618));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 26, 42, 795, DateTimeKind.Utc).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 26, 42, 795, DateTimeKind.Utc).AddTicks(2737));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 26, 42, 795, DateTimeKind.Utc).AddTicks(2739));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 22, 47, 384, DateTimeKind.Utc).AddTicks(9180));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 22, 47, 384, DateTimeKind.Utc).AddTicks(9891));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 22, 47, 384, DateTimeKind.Utc).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 22, 47, 384, DateTimeKind.Utc).AddTicks(9895));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 22, 47, 384, DateTimeKind.Utc).AddTicks(9897));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 22, 47, 384, DateTimeKind.Utc).AddTicks(9898));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 22, 47, 384, DateTimeKind.Utc).AddTicks(9900));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 22, 47, 384, DateTimeKind.Utc).AddTicks(9901));
        }
    }
}
