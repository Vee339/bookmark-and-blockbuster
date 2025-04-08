using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookmarkAndBlockbuster.Data.Migrations
{
    /// <inheritdoc />
    public partial class screening : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Halls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    ScreeningId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: true),
                    ScreeningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.ScreeningId);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Halls_MovieId",
                table: "Halls",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Movies_MovieId",
                table: "Halls",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Movies_MovieId",
                table: "Halls");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropIndex(
                name: "IX_Halls_MovieId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Halls");
        }
    }
}
