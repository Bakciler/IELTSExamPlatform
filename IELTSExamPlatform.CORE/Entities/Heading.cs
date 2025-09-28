using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class Heading : BaseEntity
{
    public string Symbol { get; set; }
    public string Content { get; set; }
    public Guid ReadingPassageId { get; set; }
    public ReadingPassage ReadingPassage { get; set; }
}
