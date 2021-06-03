using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bookStory.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "047c047a-9d73-4e37-b517-ed08dfc2ca45");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dc5aadcc-21ed-45fd-8a0a-1f502974c5b3", "AQAAAAEAACcQAAAAEII7NjD09mm0sMW6jtQwaZhzTW0c7ASJKseFO/fzlV2tJHbBd5099e/NlD6xpNywRg==" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateProject",
                value: new DateTime(2021, 6, 4, 2, 7, 36, 9, DateTimeKind.Local).AddTicks(3363));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateProject",
                value: new DateTime(2021, 6, 4, 2, 7, 36, 10, DateTimeKind.Local).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateProject",
                value: new DateTime(2021, 6, 4, 2, 7, 36, 10, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 6, 4, 2, 7, 36, 10, DateTimeKind.Local).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 6, 4, 2, 7, 36, 10, DateTimeKind.Local).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 6, 4, 2, 7, 36, 10, DateTimeKind.Local).AddTicks(7361));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "7352385c-5446-480d-9794-dd23cefc6ad9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5f616326-b3ab-4dba-b760-8c8f67a6bc2e", "AQAAAAEAACcQAAAAEE7Ss90zEsAaXxyAI5/F+dX4+KzXqYHiLops+NZCQIaspryXybsEF/KaD10VUDML9w==" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateProject",
                value: new DateTime(2021, 6, 4, 2, 1, 49, 718, DateTimeKind.Local).AddTicks(7853));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateProject",
                value: new DateTime(2021, 6, 4, 2, 1, 49, 719, DateTimeKind.Local).AddTicks(7493));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateProject",
                value: new DateTime(2021, 6, 4, 2, 1, 49, 719, DateTimeKind.Local).AddTicks(7511));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 6, 4, 2, 1, 49, 719, DateTimeKind.Local).AddTicks(9417));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 6, 4, 2, 1, 49, 719, DateTimeKind.Local).AddTicks(9743));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 6, 4, 2, 1, 49, 719, DateTimeKind.Local).AddTicks(9748));
        }
    }
}
