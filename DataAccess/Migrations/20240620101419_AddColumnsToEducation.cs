using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Education");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Education",
                newName: "Speciality");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Education",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Education");

            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "Education",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Education",
                type: "text",
                nullable: true);
        }
    }
}
