using Microsoft.EntityFrameworkCore.Migrations;

namespace GamePlace.Migrations
{
    public partial class gameplacemi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "78b1b46b-cd12-446d-ac3d-1ab40f47ed5a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "7bad6ea8-b26c-40ae-a0da-68a73e1721de");
        }
    }
}
