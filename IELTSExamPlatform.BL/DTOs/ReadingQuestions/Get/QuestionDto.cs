namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.Get
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
    }

}
