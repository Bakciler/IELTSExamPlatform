namespace IELTSExamPlatform.BL.DTOs.Reading;
public class CreateReadingPassageDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<CreateReadingParagraphsDto> Paragraphs { get; set; } = new List<CreateReadingParagraphsDto>();
}
