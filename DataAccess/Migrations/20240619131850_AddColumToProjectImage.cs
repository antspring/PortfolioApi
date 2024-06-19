using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumToProjectImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectImages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImages",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImages_Projects_ProjectId",
                table: "ProjectImages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImages_Projects_ProjectId",
                table: "ProjectImages");

            migrationBuilder.DropIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImages");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectImages");
        }
    }
}
