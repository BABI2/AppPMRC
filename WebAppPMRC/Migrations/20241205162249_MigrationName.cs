using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppPMRC.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 19, 46, 960, DateTimeKind.Utc).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 19, 46, 960, DateTimeKind.Utc).AddTicks(5494));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 19, 46, 960, DateTimeKind.Utc).AddTicks(5498));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 19, 46, 960, DateTimeKind.Utc).AddTicks(5500));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 19, 46, 960, DateTimeKind.Utc).AddTicks(5502));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 19, 46, 960, DateTimeKind.Utc).AddTicks(5504));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 19, 46, 960, DateTimeKind.Utc).AddTicks(5506));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 16, 19, 46, 960, DateTimeKind.Utc).AddTicks(5509));
        }
    }
}
