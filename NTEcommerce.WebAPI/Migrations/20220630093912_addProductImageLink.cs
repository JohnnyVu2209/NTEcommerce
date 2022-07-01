using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class addProductImageLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "a469c92f-acb9-407d-b697-28b176dd2035");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "73e066ce-d3ff-4301-894b-51b60824783e", new DateTime(2022, 6, 30, 16, 39, 6, 796, DateTimeKind.Local).AddTicks(4548), "AQAAAAEAACcQAAAAED3ZJSx9kYtQvmAn5k5xztDVv7D6YFlsTZfmUUnaJz+nnyl+MhjMrrV2d4EcHNJB/A==", new DateTime(2022, 6, 30, 16, 39, 6, 796, DateTimeKind.Local).AddTicks(4591) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "74963770-bf11-464f-bd18-452b219d086f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "95c9564d-1a08-4b68-842d-3a8d3cbd288e", new DateTime(2022, 6, 29, 23, 52, 5, 189, DateTimeKind.Local).AddTicks(6551), "AQAAAAEAACcQAAAAEMPnK5+plFyEwXfpSQELuDCQimReSngTRAaB8ub7qEcj/cJasehO87Qe0tK4CQkJtw==", new DateTime(2022, 6, 29, 23, 52, 5, 189, DateTimeKind.Local).AddTicks(6573) });
        }
    }
}
