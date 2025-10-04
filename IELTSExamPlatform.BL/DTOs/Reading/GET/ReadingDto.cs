namespace IELTSExamPlatform.BL.DTOs.Reading.GET;
public class ReadingDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<ReadingPassageDto> ReadingPassages { get; set; }
}
