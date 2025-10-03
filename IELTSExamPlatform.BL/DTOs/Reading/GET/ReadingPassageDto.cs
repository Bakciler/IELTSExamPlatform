namespace IELTSExamPlatform.BL.DTOs.Reading.GET;
public class ReadingPassageDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<ReadingParagraphDto>? ReadingParagrahs { get; set; }
}
