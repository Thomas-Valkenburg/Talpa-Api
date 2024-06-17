using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talpa_Api.Migrations
{
    /// <inheritdoc />
    public partial class updatemanytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollDates_Vote_VoteId",
                table: "PollDates");

            migrationBuilder.DropIndex(
                name: "IX_PollDates_VoteId",
                table: "PollDates");

            migrationBuilder.DropColumn(
                name: "VoteId",
                table: "PollDates");

            migrationBuilder.CreateTable(
                name: "PollDateVote",
                columns: table => new
                {
                    DatesId = table.Column<int>(type: "int", nullable: false),
                    VotesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollDateVote", x => new { x.DatesId, x.VotesId });
                    table.ForeignKey(
                        name: "FK_PollDateVote_PollDates_DatesId",
                        column: x => x.DatesId,
                        principalTable: "PollDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollDateVote_Vote_VotesId",
                        column: x => x.VotesId,
                        principalTable: "Vote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PollDateVote_VotesId",
                table: "PollDateVote",
                column: "VotesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollDateVote");

            migrationBuilder.AddColumn<int>(
                name: "VoteId",
                table: "PollDates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PollDates_VoteId",
                table: "PollDates",
                column: "VoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollDates_Vote_VoteId",
                table: "PollDates",
                column: "VoteId",
                principalTable: "Vote",
                principalColumn: "Id");
        }
    }
}
