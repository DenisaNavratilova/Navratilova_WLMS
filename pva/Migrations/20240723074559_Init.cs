using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pva.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloodLevel = table.Column<int>(type: "int", nullable: false),
                    DroughtLevel = table.Column<int>(type: "int", nullable: false),
                    TimeOutinMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    ValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.ValueId);
                    table.ForeignKey(
                        name: "FK_Values_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "DroughtLevel", "FloodLevel", "Name", "TimeOutinMinutes" },
                values: new object[,]
                {
                    { 1, 40, 90, "Colorado River", 35 },
                    { 2, 40, 90, "Mississippi", 35 },
                    { 3, 1, 70, "Amargosa", 95 },
                    { 4, 1, 70, "Gila", 95 },
                    { 5, 35, 85, "Hudson", 65 },
                    { 6, 30, 80, "Columbia", 45 },
                    { 7, 25, 75, "Snake", 45 },
                    { 8, 38, 88, "Arkansas", 75 },
                    { 9, 27, 77, "Rio Grande", 25 },
                    { 10, 30, 80, "Yukon", 65 }
                });

            migrationBuilder.InsertData(
                table: "Values",
                columns: new[] { "ValueId", "Level", "StationId", "Timestamp" },
                values: new object[,]
                {
                    { 1, 41, 1, new DateTime(2024, 7, 23, 9, 45, 59, 400, DateTimeKind.Local).AddTicks(2095) },
                    { 2, 65, 2, new DateTime(2024, 7, 23, 9, 45, 59, 400, DateTimeKind.Local).AddTicks(2142) },
                    { 3, 27, 3, new DateTime(2024, 7, 23, 9, 45, 59, 400, DateTimeKind.Local).AddTicks(2145) },
                    { 4, 73, 4, new DateTime(2024, 7, 23, 9, 45, 59, 400, DateTimeKind.Local).AddTicks(2148) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_StationId",
                table: "Values",
                column: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
