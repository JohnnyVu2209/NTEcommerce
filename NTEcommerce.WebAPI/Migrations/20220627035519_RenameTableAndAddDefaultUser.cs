using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class RenameTableAndAddDefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"), "b496a655-1c47-40ad-ad07-14b90d0d2d38", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FullName", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"), 0, "6c7730b5-1d9b-4d47-847d-dc5e30ba2948", new DateTime(2022, 6, 27, 10, 55, 17, 757, DateTimeKind.Local).AddTicks(1181), null, false, "Hương Khôn Vũ", false, false, null, null, null, "AQAAAAEAACcQAAAAEKTrfuqXWv6I/PmJfMHVcOMGqdrgLPtQS74Fr9VJdGpb8gCG+7ppQ+icnWT5Qf2owQ==", null, false, null, false, new DateTime(2022, 6, 27, 10, 55, 17, 757, DateTimeKind.Local).AddTicks(1214), "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"), new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"), new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"));

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
