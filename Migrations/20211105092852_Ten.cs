using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeDisney.Migrations
{
    public partial class Ten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charaters_Movies_MoviesId",
                schema: "MoviesData",
                table: "Charaters");

            migrationBuilder.DropIndex(
                name: "IX_Charaters_MoviesId",
                schema: "MoviesData",
                table: "Charaters");

            migrationBuilder.DropColumn(
                name: "MoviesId",
                schema: "MoviesData",
                table: "Charaters");

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                schema: "MoviesData",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.CharactersId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Charaters_CharactersId",
                        column: x => x.CharactersId,
                        principalSchema: "MoviesData",
                        principalTable: "Charaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "MoviesData",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_MoviesId",
                schema: "MoviesData",
                table: "CharacterMovie",
                column: "MoviesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie",
                schema: "MoviesData");

            migrationBuilder.AddColumn<int>(
                name: "MoviesId",
                schema: "MoviesData",
                table: "Charaters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charaters_MoviesId",
                schema: "MoviesData",
                table: "Charaters",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charaters_Movies_MoviesId",
                schema: "MoviesData",
                table: "Charaters",
                column: "MoviesId",
                principalSchema: "MoviesData",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
