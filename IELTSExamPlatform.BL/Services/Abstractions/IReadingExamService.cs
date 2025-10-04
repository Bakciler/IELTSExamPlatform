using IELTSExamPlatform.BL.DTOs.ReadingExam;

namespace IELTSExamPlatform.BL.Services.Abstractions;
public interface IReadingExamService
{
    Task<GetReadingExamDto> RandomReadingExam();
}