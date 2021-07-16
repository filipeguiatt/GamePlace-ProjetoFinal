using Microsoft.EntityFrameworkCore.Migrations;

namespace GamePlace.Migrations
{
    public partial class migra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "2e2fe9b2-bbc0-45ec-8062-311b9da2cb11");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "cac5bf98-e2d5-46c8-8a32-f08faac9a8b5");
        }
    }
}
