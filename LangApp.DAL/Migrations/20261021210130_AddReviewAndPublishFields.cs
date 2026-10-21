using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewAndPublishFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_AspNetUsers_UserId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_BaseWord_WordId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Languages_LangCodeId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Stage_StageId",
                table: "Progress");

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Translate",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reviewded",
                table: "Translate",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Stage",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reviewded",
                table: "Stage",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Languages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reviewded",
                table: "Languages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Category",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reviewded",
                table: "Category",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "BaseWord",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reviewded",
                table: "BaseWord",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_AspNetUsers_UserId",
                table: "Progress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_BaseWord_WordId",
                table: "Progress",
                column: "WordId",
                principalTable: "BaseWord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Languages_LangCodeId",
                table: "Progress",
                column: "LangCodeId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Stage_StageId",
                table: "Progress",
                column: "StageId",
                principalTable: "Stage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_AspNetUsers_UserId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_BaseWord_WordId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Languages_LangCodeId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Stage_StageId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Translate");

            migrationBuilder.DropColumn(
                name: "Reviewded",
                table: "Translate");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "Reviewded",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Reviewded",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Reviewded",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "BaseWord");

            migrationBuilder.DropColumn(
                name: "Reviewded",
                table: "BaseWord");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_AspNetUsers_UserId",
                table: "Progress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_BaseWord_WordId",
                table: "Progress",
                column: "WordId",
                principalTable: "BaseWord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Languages_LangCodeId",
                table: "Progress",
                column: "LangCodeId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Stage_StageId",
                table: "Progress",
                column: "StageId",
                principalTable: "Stage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
