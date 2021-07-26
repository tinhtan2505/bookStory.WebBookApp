using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bookStory.Data.Migrations
{
    public partial class RatingUB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Books",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "AppUsers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "81646818-4fbd-4056-bd56-ec44ced0258e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dbba8bbb-f84c-4c7b-b345-f42e40f44fc9", "AQAAAAEAACcQAAAAEJ/5lILUZacOJz6IrMOWjHndaHiPk9exekYGIVVRFaWhvyaDnVs9DXLuDnxNw5SGMA==" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateProject",
                value: new DateTime(2021, 7, 25, 4, 38, 24, 31, DateTimeKind.Local).AddTicks(8446));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateProject",
                value: new DateTime(2021, 7, 25, 4, 38, 24, 33, DateTimeKind.Local).AddTicks(5376));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateProject",
                value: new DateTime(2021, 7, 25, 4, 38, 24, 33, DateTimeKind.Local).AddTicks(5502));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 7, 25, 4, 38, 24, 33, DateTimeKind.Local).AddTicks(9247));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 7, 25, 4, 38, 24, 33, DateTimeKind.Local).AddTicks(9887));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 7, 25, 4, 38, 24, 33, DateTimeKind.Local).AddTicks(9895));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "7b1e112e-cc6d-48d3-8ae4-7c16e4789b5b");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fbd47ec8-7299-4d7a-96e1-4906e9721b5c", "AQAAAAEAACcQAAAAEGKwcR/8gSpsZWb1RQvXB9HBKwsXfPwaiLfLiTiaAnLKsOBWUYCPqvP7mdbSJfxAWQ==" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateProject",
                value: new DateTime(2021, 7, 25, 0, 51, 41, 148, DateTimeKind.Local).AddTicks(93));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateProject",
                value: new DateTime(2021, 7, 25, 0, 51, 41, 150, DateTimeKind.Local).AddTicks(7283));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateProject",
                value: new DateTime(2021, 7, 25, 0, 51, 41, 150, DateTimeKind.Local).AddTicks(7331));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 7, 25, 0, 51, 41, 151, DateTimeKind.Local).AddTicks(2278));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 7, 25, 0, 51, 41, 151, DateTimeKind.Local).AddTicks(2918));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 7, 25, 0, 51, 41, 151, DateTimeKind.Local).AddTicks(2927));
        }
    }
}
