using Microsoft.EntityFrameworkCore.Migrations;

namespace GamePlace.Migrations
{
    public partial class gmeplace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "4dfae7a1-c93c-43bf-89bc-d27667357813");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "35eb11cd-9271-41ec-831c-712f3f255816");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "20c54dfe-a155-49b9-af3d-048ed0ec0df9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "1706e914-7aa7-4c2a-b7da-65f9b4d0db55");
        }
    }
}
