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

    Task<ReadingDto?> GetByIdAsync(Guid id);

    Task UpdateReadingAsync(Guid readingId, ReadingDto updatedReading);
    Task DeletePassageAsync(Guid passageId);
    Task DeleteParagraphAsync(Guid paragraphId);
}
