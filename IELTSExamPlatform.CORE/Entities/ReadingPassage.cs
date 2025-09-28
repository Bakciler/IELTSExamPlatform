using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class ReadingPassage : BaseEntity
{
    public string Title { get; set; }   
    public string Description { get; set; }
    public string ReadingId { get; set; }
    public Reading Reading { get; set; }
    public ICollection<ReadingParagrahs> ReadingParagrahs { get; set; }= new List<ReadingParagrahs>();
    public ICollection<Heading> Headings { get; set; } = new List<Heading>();


}
