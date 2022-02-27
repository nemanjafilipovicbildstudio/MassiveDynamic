using Microsoft.EntityFrameworkCore.Migrations;

namespace MassiveDynamic.Data.Migrations
{
    public partial class SeedAdmin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4d8fb79e-6ee4-4f2d-8b98-bfaa008f3917");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7dd02711-11a9-49ea-8dda-235020b9d12c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "feb60722-cb6b-477a-a89d-08fdd77e5302");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "7eef01bb-7fb4-45ae-9a90-f8541fa46c1f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "SecurityStamp" },
                values: new object[] { "ff02178c-6716-45b8-b29e-28cd1068f632", true, "096aa94c-3ba9-4ea0-962d-773e145fdc29" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "88ff1742-a963-4947-9c04-3e7750ef838b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "67459d36-17e8-46f5-8e58-f3f5940cbaf2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "f7d8b251-fd81-468a-8ace-e692d3c4ef61");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "22dec8fa-23a9-424c-a4bf-016a190e6130");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "SecurityStamp" },
                values: new object[] { "01a2356b-ad14-4dd8-99b9-99add1f52ac8", false, "55e37a0c-108f-439b-ae3f-83897e4236ee" });
        }
    }
}
