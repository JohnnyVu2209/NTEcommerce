using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class addCategoryParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "41cb85b9-fadf-48e3-871e-d767d423f62d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "a97f22db-ae1b-4b20-8a1e-e35b11d2470b", new DateTime(2022, 6, 27, 13, 38, 18, 227, DateTimeKind.Local).AddTicks(1595), "AQAAAAEAACcQAAAAEG+Xsg5n7DStf8b6mVHOfzyyUjRLiv6AIex7Loi2VfHj9rZmaK+ye6Ap0pFuQcf4CA==", new DateTime(2022, 6, 27, 13, 38, 18, 227, DateTimeKind.Local).AddTicks(1614) });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
