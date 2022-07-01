using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class AddBaseDataToReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Reviews");

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
    }
}
