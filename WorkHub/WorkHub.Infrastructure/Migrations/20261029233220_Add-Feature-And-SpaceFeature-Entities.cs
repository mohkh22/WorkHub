using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFeatureAndSpaceFeatureEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceFeatures",
                columns: table => new
                {
                    SpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    SpaceId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceFeatures", x => new { x.SpaceId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_SpaceFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpaceFeatures_Features_FeatureId1",
                        column: x => x.FeatureId1,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaceFeatures_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpaceFeatures_Spaces_SpaceId1",
                        column: x => x.SpaceId1,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Features_Name",
                table: "Features",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpaceFeatures_FeatureId",
                table: "SpaceFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceFeatures_FeatureId1",
                table: "SpaceFeatures",
                column: "FeatureId1");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceFeatures_SpaceId1",
                table: "SpaceFeatures",
                column: "SpaceId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpaceFeatures");

            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
