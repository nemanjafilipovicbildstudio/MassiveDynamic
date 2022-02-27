using Microsoft.EntityFrameworkCore.Migrations;

namespace MassiveDynamic.Data.Migrations
{
    public partial class SeedAdmin3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f596e7df-3a3d-4119-a534-50649a85bc88");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b66c8a46-6ceb-4996-b5d2-c7f515a6b041");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "b6f4f518-1a81-4b59-a8e9-327041f6dfdb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "e7c5ef18-c1fc-41d6-b2bd-c3be30e16906");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "SecurityStamp" },
                values: new object[] { "5b15e2bd-8a16-4b4b-87ed-9b94c5197ac1", "ADMIN@MASSIVEDYNAMIC.COM", "ADMIN", "55909cec-65df-4370-b997-a9d4757a9c16" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "SecurityStamp" },
                values: new object[] { "ff02178c-6716-45b8-b29e-28cd1068f632", null, null, "096aa94c-3ba9-4ea0-962d-773e145fdc29" });
        }
    }
}
