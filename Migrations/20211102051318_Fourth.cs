using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeDisney.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "MoviesData",
                table: "Charaters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                schema: "MoviesData",
                table: "Charaters",
                columns: new[] { "Id", "Age", "History", "Image", "MoviesAssociated", "MoviesId", "Name", "Weight" },
                values: new object[] { 4, 17, "Thriler Bark", "ghost", "One Piece Stampede", null, "Perona", 45 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "MoviesData",
                table: "Charaters",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                schema: "MoviesData",
                table: "Charaters",
                columns: new[] { "Id", "Age", "History", "Image", "MoviesAssociated", "MoviesId", "Name", "Weight" },
                values: new object[] { 1, 19, "IdontKnow", "hola", "One Piece", null, "Luffy", 40 });
        }
    }
}
