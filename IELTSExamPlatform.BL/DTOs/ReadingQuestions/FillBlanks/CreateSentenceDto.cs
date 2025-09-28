namespace IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
public class CreateSentenceDto
{
    public string Content { get; set; }   // cümlə mətni (istersen ekleyebilirsin)
    public List<CreateBlankDto> Blanks { get; set; } = new();
}
