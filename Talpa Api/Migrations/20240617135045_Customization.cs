using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talpa_Api.Migrations
{
    /// <inheritdoc />
    public partial class Customization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customization",
                columns: table => new
                {
                    Color1 = table.Column<string>(type: "longtext", nullable: false),
                    Color2 = table.Column<string>(type: "longtext", nullable: true),
                    Color3 = table.Column<string>(type: "longtext", nullable: true),
                    Gradient = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LogoPath = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customization");
        }
    }
}
