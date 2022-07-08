using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class addQuantityToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "ad18f3fc-95d4-45ef-8a71-a165a46fac8e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "94c9abf1-dd9c-44a0-8c08-75bb31fd5a8a", new DateTime(2022, 7, 8, 12, 8, 17, 8, DateTimeKind.Local).AddTicks(2722), "AQAAAAEAACcQAAAAECaZYJ0dxwZ878d0EH/OPauUm0Vbm1GWQf5ETsmghZ5eZUsHtHSut1kYIi27hJwcXQ==", new DateTime(2022, 7, 8, 12, 8, 17, 8, DateTimeKind.Local).AddTicks(2734) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

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
    }
}
