using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IELTSExamPlatform.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditedEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionRange",
                table: "ReadingQuestions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionRange",
                table: "ReadingQuestions",
                type: "text",
                nullable: true);
        }
    }
}
