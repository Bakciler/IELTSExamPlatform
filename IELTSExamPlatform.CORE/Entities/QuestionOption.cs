using IELTSExamPlatform.CORE.Entities.Common;
using Microsoft.Identity.Client;

namespace IELTSExamPlatform.CORE.Entities;
public class QuestionOption : BaseEntity
{    public char Code { get; set; }
    public string Content { get; set; }
    public Guid ChoiceQuestionId { get; set; }
    public ChoiceQuestion ChoiceQuestion { get; set; }
    public bool IsCorrect { get; set; }
}
