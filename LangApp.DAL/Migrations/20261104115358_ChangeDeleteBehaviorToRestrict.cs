using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehaviorToRestrict : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Translate_BaseWord_WordId",
                table: "Translate");

            migrationBuilder.DropForeignKey(
                name: "FK_Translate_Languages_LanguageId",
                table: "Translate");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Translate_BaseWord_WordId",
                table: "Translate",
                column: "WordId",
                principalTable: "BaseWord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translate_Languages_LanguageId",
                table: "Translate",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Translate_BaseWord_WordId",
                table: "Translate");

            migrationBuilder.DropForeignKey(
                name: "FK_Translate_Languages_LanguageId",
                table: "Translate");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Translate_BaseWord_WordId",
                table: "Translate",
                column: "WordId",
                principalTable: "BaseWord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translate_Languages_LanguageId",
                table: "Translate",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
