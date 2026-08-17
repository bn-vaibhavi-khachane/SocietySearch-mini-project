using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocietySearch.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Societies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstablishmentYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocietyImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Societies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocietyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_Societies_SocietyId",
                        column: x => x.SocietyId,
                        principalTable: "Societies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocietyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitNumber = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Societies_SocietyId",
                        column: x => x.SocietyId,
                        principalTable: "Societies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Name", "SocietyId" },
                values: new object[,]
                {
                    { new Guid("0ccc5c1d-19c6-443e-a9ad-2c320acee3fa"), "Gym", null },
                    { new Guid("3928891d-c8f1-4348-ade7-8fe57534e8b4"), "Turf", null },
                    { new Guid("6fc94294-f2e9-4653-8d89-9a48c050f45f"), "Library", null },
                    { new Guid("7d87193a-c059-428d-b1c8-2aa2df9874dc"), "CCTV", null },
                    { new Guid("8de2be25-2200-4754-ac84-1e7955bcccb8"), "Swimming Pool", null },
                    { new Guid("989e00b5-0271-48b2-aa8a-14ace9d6b277"), "Parking", null },
                    { new Guid("d0847646-fce2-4f68-80d3-cf28caac2d89"), "Clubhouse", null },
                    { new Guid("fdb8099b-54a2-4b55-8a00-575fc2c20130"), "Children's Play Area", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_SocietyId",
                table: "Amenities",
                column: "SocietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_SocietyId",
                table: "Units",
                column: "SocietyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Societies");
        }
    }
}
