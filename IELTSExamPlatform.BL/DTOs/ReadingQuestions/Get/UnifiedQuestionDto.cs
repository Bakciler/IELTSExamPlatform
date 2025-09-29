using IELTSExamPlatform.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.Get
{
    public class UnifiedQuestionDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public int Order { get; set; }

        // Optional fields
        public string? BooleanContent { get; set; }
        public List<QuestionOptionDto>? Options { get; set; }
        public List<SentenceDto>? Sentences { get; set; }
        public Guid? HeadingId { get; set; }
    }
}
