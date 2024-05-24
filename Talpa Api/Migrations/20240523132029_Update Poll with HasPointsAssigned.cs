using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talpa_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePollwithHasPointsAssigned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PointsAssigned",
                table: "Poll",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsAssigned",
                table: "Poll");
        }
    }
}
