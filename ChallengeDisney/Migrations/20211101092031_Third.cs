using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeDisney.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SeriesAssociated",
                schema: "MoviesData",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                schema: "MoviesData",
                table: "Genres",
                columns: new[] { "Id", "Image", "Name", "SeriesAssociated" },
                values: new object[] { 5, "holaaaa", "Anime", "One Piece" });

            migrationBuilder.InsertData(
                schema: "MoviesData",
                table: "Movies",
                columns: new[] { "Id", "CharacterAssociated", "CreationDate", "GenresId", "Image", "Qualification", "Title" },
                values: new object[] { 3, "Marty McFly", "1985", null, "holaa", 5, "Back to Future" });

            migrationBuilder.InsertData(
                schema: "MoviesData",
                table: "Movies",
                columns: new[] { "Id", "CharacterAssociated", "CreationDate", "GenresId", "Image", "Qualification", "Title" },
                values: new object[] { 4, "Luffy", "2019", null, "holaaa", 5, "One Piece Stampede" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "MoviesData",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "MoviesData",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "MoviesData",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<int>(
                name: "SeriesAssociated",
                schema: "MoviesData",
                table: "Genres",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
