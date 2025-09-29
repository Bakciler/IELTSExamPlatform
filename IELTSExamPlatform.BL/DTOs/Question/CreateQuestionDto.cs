using IELTSExamPlatform.BL.DTOs.ReadingQuestions.Choice;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.CORE.Enums;

namespace IELTSExamPlatform.BL.DTOs.Question
{
    public class CreateQuestionDto
    {
        public Guid ReadingPassageId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }

        public List<CreateSentenceDto>? Sentences { get; set; } // FillInTheBlank üçün
        public List<CreateOptionDto>? Options { get; set; }     // Choice üçün
        public string? BooleanContent { get; set; }             // Boolean üçün
        public Guid? HeadingId { get; set; }                    // MatchHeading üçün
    }
}
