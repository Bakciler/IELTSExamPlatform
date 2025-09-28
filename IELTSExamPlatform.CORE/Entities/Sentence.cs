using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class Sentence : ReadingQuestion
{
    public int FillInTheBlankId { get; set; }
    public FillInTheBlank FillInTheBlank { get; set; }
    public ICollection<Blank> Blanks { get; set; } = new List<Blank>();
}
