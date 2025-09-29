using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.Get
{
    public class FillInTheBlankDto
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string QuestionText { get; set; }
        public List<SentenceDto> Sentences { get; set; }
    }
}
