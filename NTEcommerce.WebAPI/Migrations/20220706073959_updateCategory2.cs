using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class updateCategory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "a7237d10-82af-4ad2-9d01-a42c58e1e725");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "2641757e-550c-409c-a2ed-08ff792009c4", new DateTime(2022, 7, 6, 14, 39, 56, 932, DateTimeKind.Local).AddTicks(6686), "AQAAAAEAACcQAAAAEF/+dVsKc7fFYP2uPHmqG5cNX2sAOp008mvm7jalsbYfGhfFGQHuh7AfuUctIUqFtA==", new DateTime(2022, 7, 6, 14, 39, 56, 932, DateTimeKind.Local).AddTicks(6708) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "87becfad-d360-4cfa-9108-87b27d724272");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "ed9676e5-a205-44da-bde6-37c79caae45a", new DateTime(2022, 7, 6, 11, 33, 2, 438, DateTimeKind.Local).AddTicks(9454), "AQAAAAEAACcQAAAAENkEp66gN/m44tgZM+81C+0AA+hUgjU9eh+orPHuJmksRhj6SLI3uSFe1KxFy7QtkA==", new DateTime(2022, 7, 6, 11, 33, 2, 438, DateTimeKind.Local).AddTicks(9471) });
        }
    }
}
