using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class ReadingParagrahs : BaseEntity
{
    public char Key { get; set; }
    public string Content { get; set; }
    public string ReadingPassageId { get; set; }
    public ReadingPassage ReadingPassage { get; set; }
}
