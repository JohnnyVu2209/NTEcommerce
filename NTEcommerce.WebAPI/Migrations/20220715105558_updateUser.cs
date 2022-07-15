using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTEcommerce.WebAPI.Migrations
{
    public partial class updateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "4b8d2ae6-6b52-4345-9c13-45bc0cf958dd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "32bbb4bd-90b3-4a73-8c42-3b340958e544", new DateTime(2022, 7, 15, 17, 55, 57, 651, DateTimeKind.Local).AddTicks(3756), "AQAAAAEAACcQAAAAEIhLZFAck/nuuM6mcv3e2MuypKSEfHJC54/sVQ04+ZVcGnsoepIjT8v4hlsWpKkf8g==", new DateTime(2022, 7, 15, 17, 55, 57, 651, DateTimeKind.Local).AddTicks(3772) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                column: "ConcurrencyStamp",
                value: "eaf98c42-d2bb-42f2-8256-1eda36eee001");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "UpdatedDate" },
                values: new object[] { "2d6d23b5-6125-475a-aa7f-fd957169f825", new DateTime(2022, 7, 13, 10, 47, 50, 481, DateTimeKind.Local).AddTicks(9990), "AQAAAAEAACcQAAAAEG2TVUAYeITriy27LjQzzZFdYnRMvGH8UB10rfQViu+7oz/Cg2mI7kSdsK5GB3TSvg==", new DateTime(2022, 7, 13, 10, 47, 50, 482, DateTimeKind.Local).AddTicks(3) });
        }
    }
}
