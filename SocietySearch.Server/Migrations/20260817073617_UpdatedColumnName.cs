using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySearch.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Units",
                newName: "AvailabilityStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailabilityStatus",
                table: "Units",
                newName: "Status");
        }
    }
}
