using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class Blank : BaseEntity
{
    public Guid SentenceId { get; set; }
    public Sentence Sentence { get; set; }
    public string CorrectAnswer { get; set; }
}
