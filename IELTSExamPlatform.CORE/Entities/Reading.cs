using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class Reading : BaseEntity
{
    public ICollection<ReadingPassage> ReadingPassages { get; set; }
}
