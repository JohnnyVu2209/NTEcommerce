using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class updateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalProducts",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalProducts",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "7734ae3c-276d-4795-b4a9-973508348124");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "ce7928c9-f1e3-4a2f-9a2f-ef98a36129f6", new DateTime(2022, 7, 1, 12, 41, 7, 903, DateTimeKind.Local).AddTicks(3462), "AQAAAAEAACcQAAAAEHYZY10WuUz/hjMqXexg/AMC2ZJn+kSjNjb39JF1KZqleDaAecyCGq3K3bIdv3wZhw==", new DateTime(2022, 7, 1, 12, 41, 7, 903, DateTimeKind.Local).AddTicks(3487) });
        }
    }
}
