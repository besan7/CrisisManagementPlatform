using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrisisManagementPlatform.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AffectedAreas",
                columns: table => new
                {
                    AffectedAreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffectedAreas", x => x.AffectedAreaId);
                });

            migrationBuilder.CreateTable(
                name: "CrisisCategories",
                columns: table => new
                {
                    CrisisCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrisisCategories", x => x.CrisisCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Crises",
                columns: table => new
                {
                    CrisisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CrisisCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crises", x => x.CrisisId);
                    table.ForeignKey(
                        name: "FK_Crises_CrisisCategories_CrisisCategoryId",
                        column: x => x.CrisisCategoryId,
                        principalTable: "CrisisCategories",
                        principalColumn: "CrisisCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrisisAffectedAreas",
                columns: table => new
                {
                    CrisisId = table.Column<int>(type: "int", nullable: false),
                    AffectedAreaId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrisisAffectedAreas", x => new { x.CrisisId, x.AffectedAreaId });
                    table.ForeignKey(
                        name: "FK_CrisisAffectedAreas_AffectedAreas_AffectedAreaId",
                        column: x => x.AffectedAreaId,
                        principalTable: "AffectedAreas",
                        principalColumn: "AffectedAreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrisisAffectedAreas_Crises_CrisisId",
                        column: x => x.CrisisId,
                        principalTable: "Crises",
                        principalColumn: "CrisisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crises_CrisisCategoryId",
                table: "Crises",
                column: "CrisisCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CrisisAffectedAreas_AffectedAreaId",
                table: "CrisisAffectedAreas",
                column: "AffectedAreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrisisAffectedAreas");

            migrationBuilder.DropTable(
                name: "AffectedAreas");

            migrationBuilder.DropTable(
                name: "Crises");

            migrationBuilder.DropTable(
                name: "CrisisCategories");
        }
    }
}
