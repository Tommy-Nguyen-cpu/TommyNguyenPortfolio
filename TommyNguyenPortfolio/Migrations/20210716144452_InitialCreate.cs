using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TommyNguyenPortfolio.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordTable",
                columns: table => new
                {
                    PasswordTableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type:"nvarchar(max)", nullable: false),
                    ClientID = table.Column<int>(nullable: false, defaultValueSql:"((0))"),
                    PermissionLevel = table.Column<int>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordTable", x => x.PasswordTableId);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recommender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyWorkedFor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationToStudent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRecommended = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordTableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommendationDatabase_PasswordTable_PasswordTableId",
                        column: x => x.PasswordTableId,
                        principalTable: "PasswordTable",
                        principalColumn: "PasswordTableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationDatabase_PasswordTableId",
                table: "RecommendationDatabase",
                column: "PasswordTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecommendationDatabase");

            migrationBuilder.DropTable(
                name: "PasswordTable");
        }
    }
}
