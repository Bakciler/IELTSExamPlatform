namespace IELTSExamPlatform.BL.DTOs.Reading;
public class CreateReadingDto
{
    public List<CreateReadingPassageDto> Passages { get; set; } = new List<CreateReadingPassageDto>();
}
