namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.ChoiceQuestions;
public class QuestionOptionCreateDto
{
    public char Code { get; set; }
    public string Content { get; set; }
    public bool IsCorrect { get; set; }
}
