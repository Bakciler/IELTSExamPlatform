using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IELTSExamPlatform.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Updated_ReadingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MatchHeadingsQuestions_ReadingPassageId",
                table: "MatchHeadingsQuestions",
                column: "ReadingPassageId");

            migrationBuilder.CreateIndex(
                name: "IX_FillInTheBlanks_ReadingPassageId",
                table: "FillInTheBlanks",
                column: "ReadingPassageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoicesQuestions_ReadingPassageId",
                table: "ChoicesQuestions",
                column: "ReadingPassageId");

            migrationBuilder.CreateIndex(
                name: "IX_BooleanQuestions_ReadingPassageId",
                table: "BooleanQuestions",
                column: "ReadingPassageId");

            migrationBuilder.AddForeignKey(
                name: "FK_BooleanQuestions_ReadingPassages_ReadingPassageId",
                table: "BooleanQuestions",
                column: "ReadingPassageId",
                principalTable: "ReadingPassages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoicesQuestions_ReadingPassages_ReadingPassageId",
                table: "ChoicesQuestions",
                column: "ReadingPassageId",
                principalTable: "ReadingPassages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FillInTheBlanks_ReadingPassages_ReadingPassageId",
                table: "FillInTheBlanks",
                column: "ReadingPassageId",
                principalTable: "ReadingPassages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchHeadingsQuestions_ReadingPassages_ReadingPassageId",
                table: "MatchHeadingsQuestions",
                column: "ReadingPassageId",
                principalTable: "ReadingPassages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooleanQuestions_ReadingPassages_ReadingPassageId",
                table: "BooleanQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoicesQuestions_ReadingPassages_ReadingPassageId",
                table: "ChoicesQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_FillInTheBlanks_ReadingPassages_ReadingPassageId",
                table: "FillInTheBlanks");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchHeadingsQuestions_ReadingPassages_ReadingPassageId",
                table: "MatchHeadingsQuestions");

            migrationBuilder.DropIndex(
                name: "IX_MatchHeadingsQuestions_ReadingPassageId",
                table: "MatchHeadingsQuestions");

            migrationBuilder.DropIndex(
                name: "IX_FillInTheBlanks_ReadingPassageId",
                table: "FillInTheBlanks");

            migrationBuilder.DropIndex(
                name: "IX_ChoicesQuestions_ReadingPassageId",
                table: "ChoicesQuestions");

            migrationBuilder.DropIndex(
                name: "IX_BooleanQuestions_ReadingPassageId",
                table: "BooleanQuestions");
        }
    }
}
