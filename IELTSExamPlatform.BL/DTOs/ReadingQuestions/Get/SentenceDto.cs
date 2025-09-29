using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.Get
{
    public class SentenceDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public List<BlankDto> Blanks { get; set; }
    }
}
