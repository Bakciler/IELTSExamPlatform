using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.Reading.GET;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.CORE.Entities;

namespace IELTSExamPlatform.BL.Services.Abstractions;
public interface IReadingService
{
    Task CreateAsyncReading(CreateReadingDto dto);

    Task AddFillInTheBlankQuestion(CreateFillInTheBlankDto dto);
    Task<List<ReadingDto>> GetAllAsync();
}
