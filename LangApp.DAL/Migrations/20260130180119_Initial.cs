using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseWord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NormalizedWord = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayWord = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseWord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LangCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StageName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseWordCategory",
                columns: table => new
                {
                    BaseWordsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseWordCategory", x => new { x.BaseWordsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BaseWordCategory_BaseWord_BaseWordsId",
                        column: x => x.BaseWordsId,
                        principalTable: "BaseWord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseWordCategory_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WordId = table.Column<Guid>(type: "uuid", nullable: false),
                    LangCodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    NormalizedTranslatedText = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayTranslatedText = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BaseWordId = table.Column<Guid>(type: "uuid", nullable: true),
                    LanguagesId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translate_BaseWord_BaseWordId",
                        column: x => x.BaseWordId,
                        principalTable: "BaseWord",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Translate_BaseWord_WordId",
                        column: x => x.WordId,
                        principalTable: "BaseWord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translate_Languages_LangCodeId",
                        column: x => x.LangCodeId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translate_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WordId = table.Column<Guid>(type: "uuid", nullable: false),
                    StageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progress_BaseWord_WordId",
                        column: x => x.WordId,
                        principalTable: "BaseWord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Progress_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Progress_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseWord_NormalizedWord",
                table: "BaseWord",
                column: "NormalizedWord",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseWordCategory_CategoriesId",
                table: "BaseWordCategory",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LangCode",
                table: "Languages",
                column: "LangCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_StageId",
                table: "Progress",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_UserId_WordId_StageId",
                table: "Progress",
                columns: new[] { "UserId", "WordId", "StageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progress_WordId",
                table: "Progress",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_Order",
                table: "Stage",
                column: "Order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stage_StageName",
                table: "Stage",
                column: "StageName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translate_BaseWordId",
                table: "Translate",
                column: "BaseWordId");

            migrationBuilder.CreateIndex(
                name: "IX_Translate_LangCodeId",
                table: "Translate",
                column: "LangCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Translate_LanguagesId",
                table: "Translate",
                column: "LanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Translate_WordId_LangCodeId",
                table: "Translate",
                columns: new[] { "WordId", "LangCodeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Login_Email",
                table: "User",
                columns: new[] { "Login", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                table: "User",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseWordCategory");

            migrationBuilder.DropTable(
                name: "Progress");

            migrationBuilder.DropTable(
                name: "Translate");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BaseWord");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
