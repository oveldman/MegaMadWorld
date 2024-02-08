using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadWorld.Backend.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddAudienceInRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Audience",
                table: "RefreshTokens",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audience",
                table: "RefreshTokens");
        }
    }
}
