using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookmarkAndBlockbuster.Data.Migrations
{
    /// <inheritdoc />
    public partial class correctmovieslog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Movies",
                table: "MoviesLogs",
                newName: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesLogs_MemberId",
                table: "MoviesLogs",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesLogs_MovieId",
                table: "MoviesLogs",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesLogs_Members_MemberId",
                table: "MoviesLogs",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesLogs_Movies_MovieId",
                table: "MoviesLogs",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesLogs_Members_MemberId",
                table: "MoviesLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesLogs_Movies_MovieId",
                table: "MoviesLogs");

            migrationBuilder.DropIndex(
                name: "IX_MoviesLogs_MemberId",
                table: "MoviesLogs");

            migrationBuilder.DropIndex(
                name: "IX_MoviesLogs_MovieId",
                table: "MoviesLogs");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MoviesLogs",
                newName: "Movies");
        }
    }
}
