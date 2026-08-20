using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySearch.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowedUnitTypeConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Units_Type_AllowedValues",
                table: "Units",
                sql: "[Type] IN ('1 BHK', '2 BHK', '3 BHK', '4 BHK', 'Penthouse', 'Studio')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Units_Type_AllowedValues",
                table: "Units");
        }
    }
}
