using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class AddIndexUniqueCategoryName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "93bdbf07-d421-4979-bb50-dc7365ddfc94");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "595ee408-2c53-4b06-8334-c3212984b04f", new DateTime(2022, 6, 29, 17, 5, 43, 705, DateTimeKind.Local).AddTicks(979), "AQAAAAEAACcQAAAAED1r6oOZTdsLm2kBCxxiotrTFVpRDddlpKc80+FMMbxm+GLPhlhskKqGD+MOpN1qIg==", new DateTime(2022, 6, 29, 17, 5, 43, 705, DateTimeKind.Local).AddTicks(1034) });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "0f1d9a2a-4adc-41d9-ae49-81b971abba14");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "1da168b0-a5c3-4acb-b9a0-991248fa5329", new DateTime(2022, 6, 27, 16, 21, 28, 361, DateTimeKind.Local).AddTicks(2213), "AQAAAAEAACcQAAAAEL0AO8/hDx80pVVCF2ryFLKwCMbceA2OtM0QPJPjHdMULHM4ug168kNGaiqQGFhR9w==", new DateTime(2022, 6, 27, 16, 21, 28, 361, DateTimeKind.Local).AddTicks(2266) });
        }
    }
}
