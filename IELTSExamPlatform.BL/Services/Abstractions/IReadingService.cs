using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;

namespace IELTSExamPlatform.BL.Services.Abstractions;
public interface IReadingService
{
    Task CreateAsyncReading(CreateReadingDto dto);

    Task AddFillInTheBlankQuestion(CreateFillInTheBlankDto dto);
}
