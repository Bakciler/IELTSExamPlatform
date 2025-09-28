using IELTSExamPlatform.CORE.Entities.Common;
using IELTSExamPlatform.CORE.Enums;

namespace IELTSExamPlatform.CORE.Entities;

public class BooleanQuestion : ReadingQuestion
{
    public string Content { get; set; }
    public CorrectMatch CorrectAnswer { get; set; }
}
