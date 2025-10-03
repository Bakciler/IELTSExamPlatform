namespace IELTSExamPlatform.CORE.Entities.Common;
public class ReadingQuestion
{
    public Guid Id { get; set; } 
    public Guid ReadingPassageId { get; set; }
    public string QuestionText { get; set; } 
    public int Order { get; set; }
}
