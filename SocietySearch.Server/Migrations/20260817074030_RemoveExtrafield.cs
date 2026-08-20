using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySearch.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtrafield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocietyLogoUrl",
                table: "Societies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SocietyLogoUrl",
                table: "Societies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
