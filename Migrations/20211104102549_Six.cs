using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeDisney.Migrations
{
    public partial class Six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "History",
                schema: "MoviesData",
                table: "Charaters",
                newName: "Story");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Story",
                schema: "MoviesData",
                table: "Charaters",
                newName: "History");
        }
    }
}
