using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talpa_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDatestoVote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
