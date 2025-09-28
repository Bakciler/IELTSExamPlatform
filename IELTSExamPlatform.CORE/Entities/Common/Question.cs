namespace IELTSExamPlatform.CORE.Entities.Common;
public class ReadingQuestion
{
    public int Id { get; set; } 
    public int ReadingPassageId { get; set; }
    public string QuestionText { get; set; } 
    public string Order { get; set; }
    public string? QuestionRange { get; set; }
}
