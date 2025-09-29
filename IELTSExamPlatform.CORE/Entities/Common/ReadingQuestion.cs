namespace IELTSExamPlatform.CORE.Entities.Common;
public class ReadingQuestion : BaseEntity
{
    public Guid ReadingPassageId { get; set; }
    public string QuestionText { get; set; } 
    public int Order { get; set; }
    public string QuestionType { get; set; }    
}
