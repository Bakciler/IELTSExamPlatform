using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class Blank : ReadingQuestion
{
    public int SentenceId { get; set; }
    public Sentence Sentence { get; set; }
    public string CorrectAnswer { get; set; }
}
