using IELTSExamPlatform.CORE.Entities.Common;
namespace IELTSExamPlatform.CORE.Entities;
public class MatchHeadingsQuestion : ReadingQuestion
{
    public string Text { get; set; }
    public Guid HeadingId { get; set; }
    public Heading Heading { get; set; }
}
