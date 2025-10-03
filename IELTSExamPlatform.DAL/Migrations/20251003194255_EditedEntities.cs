using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IELTSExamPlatform.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchHeadingsQuestions_Headings_HeadingId",
                table: "MatchHeadingsQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchHeadingsQuestions_ReadingPassages_ReadingPassageId",
                table: "MatchHeadingsQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_ChoicesQuestions_ChoiceQuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sentences_FillInTheBlanks_FillInTheBlankId",
                table: "Sentences");

            migrationBuilder.DropTable(
                name: "BooleanQuestions");

            migrationBuilder.DropTable(
                name: "ChoicesQuestions");

            migrationBuilder.DropTable(
                name: "FillInTheBlanks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchHeadingsQuestions",
                table: "MatchHeadingsQuestions");

            migrationBuilder.RenameTable(
                name: "MatchHeadingsQuestions",
                newName: "ReadingQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_MatchHeadingsQuestions_ReadingPassageId",
                table: "ReadingQuestions",
                newName: "IX_ReadingQuestions_ReadingPassageId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchHeadingsQuestions_HeadingId",
                table: "ReadingQuestions",
                newName: "IX_ReadingQuestions_HeadingId");

            migrationBuilder.AlterColumn<char>(
                name: "Key",
                table: "ReadingParagraphs",
                type: "character(1)",
                nullable: true,
                oldClrType: typeof(char),
                oldType: "character(1)");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "ReadingQuestions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "HeadingId",
                table: "ReadingQuestions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingQuestions",
                table: "ReadingQuestions",
                column: "Id");

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
                name: "FK_ReadingQuestions_ReadingPassages_ReadingPassageId",
                table: "ReadingQuestions",
                column: "ReadingPassageId",
                principalTable: "ReadingPassages",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_ReadingQuestions_ChoiceQuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingQuestions_Headings_HeadingId",
                table: "ReadingQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingQuestions_ReadingPassages_ReadingPassageId",
                table: "ReadingQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sentences_ReadingQuestions_FillInTheBlankId",
                table: "Sentences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingQuestions",
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

            migrationBuilder.RenameTable(
                name: "ReadingQuestions",
                newName: "MatchHeadingsQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingQuestions_ReadingPassageId",
                table: "MatchHeadingsQuestions",
                newName: "IX_MatchHeadingsQuestions_ReadingPassageId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingQuestions_HeadingId",
                table: "MatchHeadingsQuestions",
                newName: "IX_MatchHeadingsQuestions_HeadingId");

            migrationBuilder.AlterColumn<char>(
                name: "Key",
                table: "ReadingParagraphs",
                type: "character(1)",
                nullable: false,
                defaultValue: '\0',
                oldClrType: typeof(char),
                oldType: "character(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "MatchHeadingsQuestions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HeadingId",
                table: "MatchHeadingsQuestions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchHeadingsQuestions",
                table: "MatchHeadingsQuestions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BooleanQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CorrectAnswer = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    QuestionRange = table.Column<string>(type: "text", nullable: true),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    ReadingPassageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanQuestions_ReadingPassages_ReadingPassageId",
                        column: x => x.ReadingPassageId,
                        principalTable: "ReadingPassages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoicesQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    QuestionRange = table.Column<string>(type: "text", nullable: true),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    ReadingPassageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoicesQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoicesQuestions_ReadingPassages_ReadingPassageId",
                        column: x => x.ReadingPassageId,
                        principalTable: "ReadingPassages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FillInTheBlanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    QuestionRange = table.Column<string>(type: "text", nullable: true),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    ReadingPassageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FillInTheBlanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FillInTheBlanks_ReadingPassages_ReadingPassageId",
                        column: x => x.ReadingPassageId,
                        principalTable: "ReadingPassages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooleanQuestions_ReadingPassageId",
                table: "BooleanQuestions",
                column: "ReadingPassageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoicesQuestions_ReadingPassageId",
                table: "ChoicesQuestions",
                column: "ReadingPassageId");

            migrationBuilder.CreateIndex(
                name: "IX_FillInTheBlanks_ReadingPassageId",
                table: "FillInTheBlanks",
                column: "ReadingPassageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchHeadingsQuestions_Headings_HeadingId",
                table: "MatchHeadingsQuestions",
                column: "HeadingId",
                principalTable: "Headings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchHeadingsQuestions_ReadingPassages_ReadingPassageId",
                table: "MatchHeadingsQuestions",
                column: "ReadingPassageId",
                principalTable: "ReadingPassages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_ChoicesQuestions_ChoiceQuestionId",
                table: "QuestionOptions",
                column: "ChoiceQuestionId",
                principalTable: "ChoicesQuestions",
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
    }
}
