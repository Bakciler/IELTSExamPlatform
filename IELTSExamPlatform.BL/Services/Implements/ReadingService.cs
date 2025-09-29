using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.Get;
using IELTSExamPlatform.BL.Services.Abstractions;
using IELTSExamPlatform.CORE.Entities;
using IELTSExamPlatform.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace IELTSExamPlatform.BL.Services.Implements;
public class ReadingService : IReadingService
{
    private readonly AppDbContext _appDbContext;

    public ReadingService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddFillInTheBlankQuestion(CreateFillInTheBlankDto dto)
    {
        int currentOrder = dto.Order;
        var fib = new FillInTheBlank
        {
            ReadingPassageId = dto.ReadingPassageId,
            QuestionText = dto.QuestionText,
            Order = dto.Order,
            QuestionRange = dto.QuestionRange,
            Sentences = dto.Sentences.Select(s => new Sentence
            {
                Text = s.Content,
                Blanks = s.Blanks.Select(b => new Blank
                {
                    CorrectAnswer = b.CorrectAnswer,
                }).ToList(),
            }).ToList()
        };

        await _appDbContext.FillInTheBlanks.AddAsync(fib);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task CreateAsyncReading(CreateReadingDto dto)
    {
        Reading reading = new Reading
        {
            ReadingPassages = dto.Passages.Select(p => new ReadingPassage
            {
                Title = p.Title,
                Description = p.Description,
                ReadingParagrahs = p.Paragraphs.Select(pg => new ReadingParagraphs
                {
                    Key = pg.Key,
                    Content = pg.Content,
                }).ToList()
            }).ToList()
        };

        await _appDbContext.Readings.AddAsync(reading);

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<FillInTheBlankDto>> GetQuestionsAsync(Guid passageId)
    {
        var questions = await _appDbContext.ReadingPassages
            .Where(r => r.Id == passageId)
            .SelectMany(r => r.FillInTheBlanks) // passage içindeki soruları direkt al
            .Select(q => new FillInTheBlankDto
            {
                Id = q.Id,
                Order = q.Order,
                QuestionText = q.QuestionText,
                Sentences = q.Sentences.Select(s => new SentenceDto
                {
                    Id = s.Id,
                    Text = s.Text,
                    Blanks = s.Blanks.Select(b => new BlankDto
                    {
                        Id = b.Id,
                        CorrectAnswer = b.CorrectAnswer
                    }).ToList()
                }).ToList()
            })
            .ToListAsync();

        return questions;
    }


    



}
