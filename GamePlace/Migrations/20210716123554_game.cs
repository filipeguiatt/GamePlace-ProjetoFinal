using Microsoft.EntityFrameworkCore.Migrations;

namespace GamePlace.Migrations
{
    public partial class game : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "389f1bf6-f393-4c2c-a52f-a87512c74e79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "6e9155e6-b79c-4d62-8a10-cca78301a771");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "219977f1-46b2-4ad0-bc5d-df4014c426fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "f111bfa3-4dbd-4bb7-8a6b-69352f15162e");
        }
    }
}
