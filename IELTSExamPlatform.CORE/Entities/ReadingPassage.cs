using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class ReadingPassage : BaseEntity
{
    public string Title { get; set; }   
    public string Description { get; set; }
    public Guid ReadingId { get; set; }
    public Reading Reading { get; set; }
    public ICollection<ReadingParagraphs> ReadingParagrahs { get; set; }= new List<ReadingParagraphs>();
    public ICollection<Heading> Headings { get; set; } = new List<Heading>();
    public ICollection<ReadingQuestion> Questions { get; set; }
}
