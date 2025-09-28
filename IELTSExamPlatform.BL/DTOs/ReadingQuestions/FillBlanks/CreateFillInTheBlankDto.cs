namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
public class CreateFillInTheBlankDto
{
    public Guid ReadingPassageId { get; set; }   // Hansi Passage üçün bu sual verilir
    public string QuestionText { get; set; }
    public int Order { get; set; }
    public string? QuestionRange { get; set; }

    public List<CreateSentenceDto> Sentences { get; set; } = new();
}
