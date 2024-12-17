using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppPMRC.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 0, 59, 284, DateTimeKind.Utc).AddTicks(3373));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 0, 59, 284, DateTimeKind.Utc).AddTicks(3707));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 0, 59, 284, DateTimeKind.Utc).AddTicks(3708));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 0, 59, 284, DateTimeKind.Utc).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 0, 59, 284, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 0, 59, 284, DateTimeKind.Utc).AddTicks(3711));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 0, 59, 284, DateTimeKind.Utc).AddTicks(3711));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 0, 59, 284, DateTimeKind.Utc).AddTicks(3712));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
