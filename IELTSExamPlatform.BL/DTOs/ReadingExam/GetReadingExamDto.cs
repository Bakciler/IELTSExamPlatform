using IELTSExamPlatform.BL.DTOs.Reading.GET;

namespace IELTSExamPlatform.BL.DTOs.ReadingExam;
public class GetReadingExamDto
{
    public Guid Id { get; set; }
    public ICollection<ReadingPassageDto> readingPassageDtos { get; set; }
}
