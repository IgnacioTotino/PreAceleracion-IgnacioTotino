using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeDisney.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "MoviesData",
                table: "Charaters",
                columns: new[] { "Id", "Age", "History", "Image", "MoviesAssociated", "MoviesId", "Name", "Weight" },
                values: new object[] { 2, 21, "Wano", "Holis", "One Piece", null, "Zoro", 70 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "MoviesData",
                table: "Charaters",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
