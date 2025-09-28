using IELTSExamPlatform.CORE.Entities.Common;
namespace IELTSExamPlatform.CORE.Entities;
public class FillInTheBlank : ReadingQuestion
{
    public ICollection<Sentence> Sentences { get; set; } = new List<Sentence>();
}
