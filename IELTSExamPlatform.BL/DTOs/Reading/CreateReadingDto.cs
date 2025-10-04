namespace IELTSExamPlatform.BL.DTOs.Reading;
public class CreateReadingDto
{
    public string Title { get; set; }
    public List<CreateReadingPassageDto> Passages { get; set; } = new List<CreateReadingPassageDto>();
}
