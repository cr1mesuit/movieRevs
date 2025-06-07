using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRevs.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieSuggestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieSuggestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SuggestedBy = table.Column<string>(type: "TEXT", nullable: true),
                    SuggestedAt = table.Column<int>(type: "INTEGER", nullable: false),
                    ReviewedByAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSuggestion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieSuggestion");
        }
    }
}
