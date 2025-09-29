using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities
{
    public class Sentence : BaseEntity
    {
        public Guid FillInTheBlankId { get; set; }   
        public FillInTheBlank FillInTheBlank { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public ICollection<Blank> Blanks { get; set; } = new List<Blank>();
    }
}
