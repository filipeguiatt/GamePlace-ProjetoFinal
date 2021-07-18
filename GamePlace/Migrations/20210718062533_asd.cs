using Microsoft.EntityFrameworkCore.Migrations;

namespace GamePlace.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "1be5625e-08ae-4c17-8d29-c5e41f3bbde7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "800f5ee7-d7e5-4f57-905a-1511c704a90b");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "431a85fd-43b8-4ed3-b6bd-18eb1302b929");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "f61ca401-bfba-45ca-aa7f-d98d0cec5d3f");
        }
    }
}
