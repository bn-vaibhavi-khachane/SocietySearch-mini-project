using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySearch.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtrafieldUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitNumber",
                table: "Units");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitNumber",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
