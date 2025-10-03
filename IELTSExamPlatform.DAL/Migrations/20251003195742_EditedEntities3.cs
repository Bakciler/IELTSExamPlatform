using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IELTSExamPlatform.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditedEntities3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_ReadingQuestions_ChoiceQuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingQuestions_Headings_HeadingId",
                table: "ReadingQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sentences_ReadingQuestions_FillInTheBlankId",
                table: "Sentences");

            migrationBuilder.DropIndex(
                name: "IX_ReadingQuestions_HeadingId",
                table: "ReadingQuestions");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ReadingQuestions");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "ReadingQuestions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ReadingQuestions");

            migrationBuilder.DropColumn(
                name: "HeadingId",
                table: "ReadingQuestions");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "ReadingQuestions");

            migrationBuilder.CreateTable(
                name: "BooleanQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CorrectAnswer = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanQuestions_ReadingQuestions_Id",
                        column: x => x.Id,
                        principalTable: "ReadingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceQuestions_ReadingQuestions_Id",
                        column: x => x.Id,
                        principalTable: "ReadingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FillInTheBlanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FillInTheBlanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FillInTheBlanks_ReadingQuestions_Id",
                        column: x => x.Id,
                        principalTable: "ReadingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchHeadingsQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    HeadingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchHeadingsQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchHeadingsQuestions_Headings_HeadingId",
                        column: x => x.HeadingId,
                        principalTable: "Headings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchHeadingsQuestions_ReadingQuestions_Id",
                        column: x => x.Id,
                        principalTable: "ReadingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchHeadingsQuestions_HeadingId",
                table: "MatchHeadingsQuestions",
                column: "HeadingId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_ChoiceQuestions_ChoiceQuestionId",
                table: "QuestionOptions",
                column: "ChoiceQuestionId",
                principalTable: "ChoiceQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sentences_FillInTheBlanks_FillInTheBlankId",
                table: "Sentences",
                column: "FillInTheBlankId",
                principalTable: "FillInTheBlanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_ChoiceQuestions_ChoiceQuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sentences_FillInTheBlanks_FillInTheBlankId",
                table: "Sentences");

            migrationBuilder.DropTable(
                name: "BooleanQuestions");

            migrationBuilder.DropTable(
                name: "ChoiceQuestions");

            migrationBuilder.DropTable(
                name: "FillInTheBlanks");

            migrationBuilder.DropTable(
                name: "MatchHeadingsQuestions");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ReadingQuestions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswer",
                table: "ReadingQuestions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ReadingQuestions",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "HeadingId",
                table: "ReadingQuestions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "ReadingQuestions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReadingQuestions_HeadingId",
                table: "ReadingQuestions",
                column: "HeadingId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_ReadingQuestions_ChoiceQuestionId",
                table: "QuestionOptions",
                column: "ChoiceQuestionId",
                principalTable: "ReadingQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingQuestions_Headings_HeadingId",
                table: "ReadingQuestions",
                column: "HeadingId",
                principalTable: "Headings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sentences_ReadingQuestions_FillInTheBlankId",
                table: "Sentences",
                column: "FillInTheBlankId",
                principalTable: "ReadingQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
