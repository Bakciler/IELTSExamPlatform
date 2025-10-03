namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.ChoiceQuestions;
public class ChoiceQuestionCreateDto
{
    public Guid ReadingPassageId { get; set; }
    public string QuestionText { get; set; }
    public int Order { get; set; }

    public ICollection<QuestionOptionCreateDto> Options { get; set; }
        = new List<QuestionOptionCreateDto>();
}
