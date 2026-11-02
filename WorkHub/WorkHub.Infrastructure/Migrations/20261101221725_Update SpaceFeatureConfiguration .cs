using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSpaceFeatureConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceFeatures_Features_FeatureId1",
                table: "SpaceFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_SpaceFeatures_Spaces_SpaceId1",
                table: "SpaceFeatures");

            migrationBuilder.DropIndex(
                name: "IX_SpaceFeatures_FeatureId1",
                table: "SpaceFeatures");

            migrationBuilder.DropIndex(
                name: "IX_SpaceFeatures_SpaceId1",
                table: "SpaceFeatures");

            migrationBuilder.DropColumn(
                name: "FeatureId1",
                table: "SpaceFeatures");

            migrationBuilder.DropColumn(
                name: "SpaceId1",
                table: "SpaceFeatures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FeatureId1",
                table: "SpaceFeatures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SpaceId1",
                table: "SpaceFeatures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SpaceFeatures_FeatureId1",
                table: "SpaceFeatures",
                column: "FeatureId1");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceFeatures_SpaceId1",
                table: "SpaceFeatures",
                column: "SpaceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceFeatures_Features_FeatureId1",
                table: "SpaceFeatures",
                column: "FeatureId1",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceFeatures_Spaces_SpaceId1",
                table: "SpaceFeatures",
                column: "SpaceId1",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
