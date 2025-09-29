using IELTSExamPlatform.BL.DTOs.Question;
using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.Get;
using IELTSExamPlatform.BL.Services.Abstractions;
using IELTSExamPlatform.CORE.Entities;
using IELTSExamPlatform.CORE.Enums;
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

    public async Task CreateQuestionAsync(CreateQuestionDto dto)
    {
        // String -> Enum parse
        if (!Enum.TryParse<QuestionType>(dto.QuestionType, true, out var type))
            throw new ArgumentException($"Invalid question type: {dto.QuestionType}");

        switch (type)
        {
            case QuestionType.Boolean:
                var booleanQ = new BooleanQuestion
                {
                    ReadingPassageId = dto.ReadingPassageId,
                    QuestionText = dto.QuestionText,
                    Content = dto.BooleanContent ?? "",
                    CorrectAnswer = CorrectMatch.Yes // misal üçün default
                };
                _appDbContext.BooleanQuestions.Add(booleanQ);
                break;
            case QuestionType.Choice:
                var choiceQ = new ChoiceQuestion
                {
                    ReadingPassageId = dto.ReadingPassageId,
                    QuestionText = dto.QuestionText,
                    QuestionOptions = dto.Options?.Select(o => new QuestionOption
                    {
                        Code = o.Code,
                        Content = o.Content,
                        IsCorrect = o.IsCorrect
                    }).ToList() ?? new List<QuestionOption>()
                };
                _appDbContext.ChoiceQuestions.Add(choiceQ);
                break;

            case QuestionType.FillInTheBlank:
                var fib = new FillInTheBlank
                {
                    ReadingPassageId = dto.ReadingPassageId,
                    QuestionText = dto.QuestionText,
                    Sentences = dto.Sentences?.Select(s => new Sentence
                    {
                        Text = s.Content,
                        Blanks = s.Blanks.Select(b => new Blank
                        {
                            CorrectAnswer = b.CorrectAnswer
                        }).ToList()
                    }).ToList() ?? new List<Sentence>()
                };
                _appDbContext.FillInTheBlanks.Add(fib);
                break;
            case QuestionType.MatchHeading:
                var mh = new MatchHeadingsQuestion
                {
                    ReadingPassageId = dto.ReadingPassageId,
                    QuestionText = dto.QuestionText,
                    HeadingId = dto.HeadingId ?? Guid.Empty
                };
                _appDbContext.MatchHeadingsQuestions.Add(mh);
                break;

            default:
                throw new ArgumentException("Unknown question type");
        }
        await _appDbContext.SaveChangesAsync();
    }
    public async Task<List<UnifiedQuestionDto>> GetAllQuestionsByPassageAsync(Guid passageId)
    {
        var booleanQuestions = await _appDbContext.BooleanQuestions
            .Where(q => q.ReadingPassageId == passageId)
            .Select(q => new UnifiedQuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionType = QuestionType.Boolean,
                Order = q.Order,
                BooleanContent = q.Content
            }).ToListAsync();

        var choiceQuestions = await _appDbContext.ChoiceQuestions
            .Where(q => q.ReadingPassageId == passageId)
            .Select(q => new UnifiedQuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionType = QuestionType.Choice,
                Order = q.Order,
                Options = q.QuestionOptions.Select(o => new QuestionOptionDto
                {
                    Code = o.Code,
                    Content = o.Content,
                    IsCorrect = o.IsCorrect
                }).ToList()
            }).ToListAsync();

        var fillInTheBlankQuestions = await _appDbContext.FillInTheBlanks
            .Where(q => q.ReadingPassageId == passageId)
            .Select(q => new UnifiedQuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionType = QuestionType.FillInTheBlank,
                Order = q.Order,
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
            }).ToListAsync();

        var matchHeadingQuestions = await _appDbContext.MatchHeadingsQuestions
            .Where(q => q.ReadingPassageId == passageId)
            .Select(q => new UnifiedQuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionType = QuestionType.MatchHeading,
                Order = q.Order,
                HeadingId = q.HeadingId
            }).ToListAsync();

        var allQuestions = booleanQuestions
            .Concat(choiceQuestions)
            .Concat(fillInTheBlankQuestions)
            .Concat(matchHeadingQuestions)
            .OrderBy(q => q.Order)
            .ToList();

        return allQuestions;
    }
}
