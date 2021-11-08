using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeDisney.Migrations
{
    public partial class Five : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CharacterAssociated",
                value: "Zoro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "MoviesData",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CharacterAssociated",
                value: "Luffy");
        }
    }
}
