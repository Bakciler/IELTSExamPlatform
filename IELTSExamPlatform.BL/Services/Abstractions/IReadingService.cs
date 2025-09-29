using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.Get;
using IELTSExamPlatform.CORE.Entities;
using System.Threading.Tasks;

namespace IELTSExamPlatform.BL.Services.Abstractions;
public interface IReadingService
{
    Task CreateAsyncReading(CreateReadingDto dto);

    Task AddFillInTheBlankQuestion(CreateFillInTheBlankDto dto);
    Task<List<FillInTheBlankDto>> GetQuestionsAsync(Guid passageId);
}
