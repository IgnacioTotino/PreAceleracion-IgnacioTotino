using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeDisney.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MoviesData");

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "MoviesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeriesAssociated = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "MoviesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualification = table.Column<int>(type: "int", nullable: false),
                    CharacterAssociated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenresId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenresId",
                        column: x => x.GenresId,
                        principalSchema: "MoviesData",
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Charaters",
                schema: "MoviesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoviesAssociated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoviesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charaters_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "MoviesData",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "MoviesData",
                table: "Charaters",
                columns: new[] { "Id", "Age", "History", "Image", "MoviesAssociated", "MoviesId", "Name", "Weight" },
                values: new object[] { 1, 19, "IdontKnow", "hola", "One Piece", null, "Luffy", 40 });

            migrationBuilder.CreateIndex(
                name: "IX_Charaters_MoviesId",
                schema: "MoviesData",
                table: "Charaters",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenresId",
                schema: "MoviesData",
                table: "Movies",
                column: "GenresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charaters",
                schema: "MoviesData");

            migrationBuilder.DropTable(
                name: "Movies",
                schema: "MoviesData");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "MoviesData");
        }
    }
}
