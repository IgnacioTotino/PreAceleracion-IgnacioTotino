using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeDisney.Migrations
{
    public partial class Seven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterAssociated",
                schema: "MoviesData",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "SeriesAssociated",
                schema: "MoviesData",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "MoviesAssociated",
                schema: "MoviesData",
                table: "Charaters");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "MoviesData",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreationDate",
                schema: "MoviesData",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CharacterAssociated",
                schema: "MoviesData",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeriesAssociated",
                schema: "MoviesData",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoviesAssociated",
                schema: "MoviesData",
                table: "Charaters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Charaters",
                keyColumn: "Id",
                keyValue: 2,
                column: "MoviesAssociated",
                value: "One Piece");

            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Charaters",
                keyColumn: "Id",
                keyValue: 4,
                column: "MoviesAssociated",
                value: "One Piece Stampede");

            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "SeriesAssociated",
                value: "One Piece");

            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CharacterAssociated", "CreationDate" },
                values: new object[] { "Marty McFly", "1985" });

            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CharacterAssociated", "CreationDate" },
                values: new object[] { "Zoro", "2019" });
        }
    }
}
