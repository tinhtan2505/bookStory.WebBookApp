using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bookStory.Data.Migrations
{
    public partial class VoteTran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Translations",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
                columns: new[] { "Date", "Rating" },
                values: new object[] { new DateTime(2021, 7, 25, 0, 51, 41, 151, DateTimeKind.Local).AddTicks(2278), 1f });

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Rating" },
                values: new object[] { new DateTime(2021, 7, 25, 0, 51, 41, 151, DateTimeKind.Local).AddTicks(2918), 2f });

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "Rating" },
                values: new object[] { new DateTime(2021, 7, 25, 0, 51, 41, 151, DateTimeKind.Local).AddTicks(2927), 3f });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Translations",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "8f319bb5-abe8-4025-96de-5b13e9165c8f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2eaa4a96-01a0-48bc-b5b0-a926dcaf5291", "AQAAAAEAACcQAAAAEJ6Cm31d8o0a21r6uu3WkesieaJPZVEU37RSs5j0kDaI0JWV8KuwF5FxMfN+7Wbc8Q==" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateProject",
                value: new DateTime(2021, 7, 15, 14, 52, 46, 818, DateTimeKind.Local).AddTicks(4746));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateProject",
                value: new DateTime(2021, 7, 15, 14, 52, 46, 819, DateTimeKind.Local).AddTicks(9190));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateProject",
                value: new DateTime(2021, 7, 15, 14, 52, 46, 819, DateTimeKind.Local).AddTicks(9219));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Rating" },
                values: new object[] { new DateTime(2021, 7, 15, 14, 52, 46, 820, DateTimeKind.Local).AddTicks(2038), 1 });

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Rating" },
                values: new object[] { new DateTime(2021, 7, 15, 14, 52, 46, 820, DateTimeKind.Local).AddTicks(2563), 2 });

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "Rating" },
                values: new object[] { new DateTime(2021, 7, 15, 14, 52, 46, 820, DateTimeKind.Local).AddTicks(2570), 3 });
        }
    }
}
