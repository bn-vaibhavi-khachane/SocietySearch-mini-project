using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySearch.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAmenitySocietyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Societies_SocietyId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_SocietyId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "SocietyId",
                table: "Amenities");

            migrationBuilder.AddColumn<string>(
                name: "AmenityIds",
                table: "Societies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocietyLogoUrl",
                table: "Societies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmenityIds",
                table: "Societies");

            migrationBuilder.DropColumn(
                name: "SocietyLogoUrl",
                table: "Societies");

            migrationBuilder.AddColumn<Guid>(
                name: "SocietyId",
                table: "Amenities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: new Guid("0ccc5c1d-19c6-443e-a9ad-2c320acee3fa"),
                column: "SocietyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: new Guid("3928891d-c8f1-4348-ade7-8fe57534e8b4"),
                column: "SocietyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: new Guid("6fc94294-f2e9-4653-8d89-9a48c050f45f"),
                column: "SocietyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: new Guid("7d87193a-c059-428d-b1c8-2aa2df9874dc"),
                column: "SocietyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: new Guid("8de2be25-2200-4754-ac84-1e7955bcccb8"),
                column: "SocietyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: new Guid("989e00b5-0271-48b2-aa8a-14ace9d6b277"),
                column: "SocietyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: new Guid("d0847646-fce2-4f68-80d3-cf28caac2d89"),
                column: "SocietyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: new Guid("fdb8099b-54a2-4b55-8a00-575fc2c20130"),
                column: "SocietyId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_SocietyId",
                table: "Amenities",
                column: "SocietyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Societies_SocietyId",
                table: "Amenities",
                column: "SocietyId",
                principalTable: "Societies",
                principalColumn: "Id");
        }
    }
}
