using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class RenameTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "41cb85b9-fadf-48e3-871e-d767d423f62d", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "NormalizedUserName", "PasswordHash", "UpdatedDate" },
                values: new object[] { "a97f22db-ae1b-4b20-8a1e-e35b11d2470b", new DateTime(2022, 6, 27, 13, 38, 18, 227, DateTimeKind.Local).AddTicks(1595), "Admin", "AQAAAAEAACcQAAAAEG+Xsg5n7DStf8b6mVHOfzyyUjRLiv6AIex7Loi2VfHj9rZmaK+ye6Ap0pFuQcf4CA==", new DateTime(2022, 6, 27, 13, 38, 18, 227, DateTimeKind.Local).AddTicks(1614) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b496a655-1c47-40ad-ad07-14b90d0d2d38", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "NormalizedUserName", "PasswordHash", "UpdatedDate" },
                values: new object[] { "6c7730b5-1d9b-4d47-847d-dc5e30ba2948", new DateTime(2022, 6, 27, 10, 55, 17, 757, DateTimeKind.Local).AddTicks(1181), null, "AQAAAAEAACcQAAAAEKTrfuqXWv6I/PmJfMHVcOMGqdrgLPtQS74Fr9VJdGpb8gCG+7ppQ+icnWT5Qf2owQ==", new DateTime(2022, 6, 27, 10, 55, 17, 757, DateTimeKind.Local).AddTicks(1214) });
        }
    }
}
