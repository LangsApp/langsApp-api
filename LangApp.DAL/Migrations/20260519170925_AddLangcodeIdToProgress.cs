using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddLangcodeIdToProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Progress_UserId_WordId_StageId",
                table: "Progress");

            migrationBuilder.AddColumn<Guid>(
                name: "LangCodeId",
                table: "Progress",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LangCodeId",
                table: "Progress",
                column: "LangCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_UserId_WordId_LangCodeId_StageId",
                table: "Progress",
                columns: new[] { "UserId", "WordId", "LangCodeId", "StageId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Languages_LangCodeId",
                table: "Progress",
                column: "LangCodeId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Languages_LangCodeId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_LangCodeId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Progress_UserId_WordId_LangCodeId_StageId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "LangCodeId",
                table: "Progress");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_UserId_WordId_StageId",
                table: "Progress",
                columns: new[] { "UserId", "WordId", "StageId" },
                unique: true);
        }
    }
}
