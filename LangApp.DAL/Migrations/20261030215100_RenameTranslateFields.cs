using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameTranslateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reviewded",
                table: "Translate",
                newName: "Reviewed");

            migrationBuilder.RenameColumn(
                name: "Reviewded",
                table: "BaseWord",
                newName: "Reviewed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reviewed",
                table: "Translate",
                newName: "Reviewded");

            migrationBuilder.RenameColumn(
                name: "Reviewed",
                table: "BaseWord",
                newName: "Reviewded");
        }
    }
}
