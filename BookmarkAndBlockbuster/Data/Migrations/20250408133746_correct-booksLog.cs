using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookmarkAndBlockbuster.Data.Migrations
{
    /// <inheritdoc />
    public partial class correctbooksLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Screenings_Id",
                table: "Screenings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_AuthorId",
                table: "Movies",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksLogs_BookId",
                table: "BooksLogs",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksLogs_MemberId",
                table: "BooksLogs",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksLogs_Books_BookId",
                table: "BooksLogs",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BooksLogs_Members_MemberId",
                table: "BooksLogs",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Authors_AuthorId",
                table: "Movies",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Halls_Id",
                table: "Screenings",
                column: "Id",
                principalTable: "Halls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksLogs_Books_BookId",
                table: "BooksLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BooksLogs_Members_MemberId",
                table: "BooksLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Authors_AuthorId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Halls_Id",
                table: "Screenings");

            migrationBuilder.DropIndex(
                name: "IX_Screenings_Id",
                table: "Screenings");

            migrationBuilder.DropIndex(
                name: "IX_Movies_AuthorId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_BooksLogs_BookId",
                table: "BooksLogs");

            migrationBuilder.DropIndex(
                name: "IX_BooksLogs_MemberId",
                table: "BooksLogs");
        }
    }
}
