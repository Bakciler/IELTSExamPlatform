using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.Choice
{
    public class CreateOptionDto
    {        
        public char Code { get; set; }        
        public string Content { get; set; }       
        public bool IsCorrect { get; set; }
    }

}
