using IELTSExamPlatform.CORE.Entities.Common;

namespace IELTSExamPlatform.CORE.Entities;
public class ChoiceQuestion : ReadingQuestion
{
    public ICollection<QuestionOption> QuestionOptions { get; set; }=new List<QuestionOption>();
}
