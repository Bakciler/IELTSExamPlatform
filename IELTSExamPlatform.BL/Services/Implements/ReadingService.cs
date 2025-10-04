using Azure.Core;
using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.Reading.GET;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.ChoiceQuestions;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.BL.Services.Abstractions;
using IELTSExamPlatform.CORE.Entities;
using IELTSExamPlatform.CORE.Entities.Common;
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
                    Order = currentOrder++,
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
            Title = dto.Title,
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
    public async Task<List<ReadingDto>> GetAllAsync()
    {
        var readings = await _appDbContext.Readings
            .Include(r => r.ReadingPassages)
            .ThenInclude(p => p.ReadingParagrahs)
            .ToListAsync();

        var readingDtos = readings.Select(r => new ReadingDto
        {
            Id = r.Id,
            Title = r.Title,
            ReadingPassages = r.ReadingPassages.Select(p => new ReadingPassageDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ReadingParagrahs = p.ReadingParagrahs
                    .OrderBy(pg => pg.Key)
                    .Select(pg => new ReadingParagraphDto
                    {
                        Id = pg.Id,
                        Key = pg.Key,
                        Content = pg.Content
                    }).ToList()
            }).ToList()
        }).ToList();

        return readingDtos;
    }
    public async Task<ReadingDto> GetByIdAsync(Guid readingId)
    {
        var reading = await _appDbContext.Readings
            .Include(r => r.ReadingPassages)
                .ThenInclude(p => p.ReadingParagrahs)
            .FirstOrDefaultAsync(r => r.Id == readingId);

        if (reading == null)
            return null;

        return new ReadingDto
        {
            Id = reading.Id,
            Title = reading.Title,
            ReadingPassages = reading.ReadingPassages.Select(p => new ReadingPassageDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ReadingParagrahs = p.ReadingParagrahs
                    .OrderBy(pg => pg.Key)
                    .Select(pg => new ReadingParagraphDto
                    {
                        Id = pg.Id,
                        Key = pg.Key,
                        Content = pg.Content
                    }).ToList()
            }).ToList()
        };
    }
    public async Task UpdateReadingAsync(Guid readingId, CreateReadingDto updatedReading)
    {
        var reading = await _appDbContext.Readings
            .Include(r => r.ReadingPassages)
                .ThenInclude(p => p.ReadingParagrahs)
            .FirstOrDefaultAsync(r => r.Id == readingId);

        if (reading == null)
            throw new Exception("Reading not found");

        // 1️⃣ Reading başlığını update et
        reading.Title = updatedReading.Title;

        // 2️⃣ Mövcud passages və paragraphs silirik
        var existingPassages = reading.ReadingPassages.ToList();
        foreach (var passage in existingPassages)
        {
            _appDbContext.ReadingParagraphs.RemoveRange(passage.ReadingParagrahs);
            _appDbContext.ReadingPassages.Remove(passage);
        }

        // 3️⃣ JSON-dan gələn passage-ları yenidən əlavə et
        foreach (var passageDto in updatedReading.Passages)
        {
            // Mövcud ID varsa onu saxla, yoxsa yeni GUID yarat
            var passageId = (passageDto.Id == Guid.Empty || passageDto.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                            ? Guid.NewGuid()
                            : passageDto.Id.Value;

            var newPassage = new ReadingPassage
            {
                Id = passageId,
                Title = passageDto.Title,
                Description = passageDto.Description,
                ReadingId = reading.Id,
                ReadingParagrahs = new List<ReadingParagraphs>()
            };

            if (passageDto.Paragraphs != null)
            {
                foreach (var pg in passageDto.Paragraphs)
                {
                    var paraId = (pg.Id == Guid.Empty || pg.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                                 ? Guid.NewGuid()
                                 : pg.Id.Value;

                    newPassage.ReadingParagrahs.Add(new ReadingParagraphs
                    {
                        Id = paraId,
                        Key = pg.Key,
                        Content = pg.Content,
                        ReadingPassageId = newPassage.Id
                    });
                }
            }

            _appDbContext.ReadingPassages.Add(newPassage);
        }

        await _appDbContext.SaveChangesAsync();
    }







    public async Task DeletePassageAsync(Guid passageId)
    {
        var passage = await _appDbContext.ReadingPassages
            .Include(p => p.ReadingParagrahs)
            .FirstOrDefaultAsync(p => p.Id == passageId);

        if (passage == null)
            throw new Exception("Passage not found");

        _appDbContext.ReadingParagraphs.RemoveRange(passage.ReadingParagrahs);
        _appDbContext.ReadingPassages.Remove(passage);

        await _appDbContext.SaveChangesAsync();
    }
    public async Task DeleteParagraphAsync(Guid paragraphId)
    {
        var paragraph = await _appDbContext.ReadingParagraphs
            .FirstOrDefaultAsync(p => p.Id == paragraphId);

        if (paragraph == null)
            throw new Exception("Paragraph not found");

        _appDbContext.ReadingParagraphs.Remove(paragraph);
        await _appDbContext.SaveChangesAsync();
    }
    public async Task<List<ReadingPassageDto>> GetAllPassagesAsyncByReadingId(Guid id)
    {
        var reading = await _appDbContext.Readings.Where(x => x.Id == id).Include(x => x.ReadingPassages).FirstOrDefaultAsync();

        var passages = reading!.ReadingPassages.Select(x => new ReadingPassageDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description
        }).ToList();

        return passages;

    }
    public async Task<Guid> CreateQuestionAsync(QuestionCreateRequestDto request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        ReadingQuestion question;

        switch (request.Type)
        {
            case "Choice":
                question = new ChoiceQuestion
                {
                    Id = Guid.NewGuid(),
                    ReadingPassageId = request.ReadingPassageId,
                    QuestionText = request.QuestionText,
                    Order = 1,
                    QuestionOptions = request.Options.Select(o => new QuestionOption
                    {
                        Id = Guid.NewGuid(),
                        Code = o.Code,
                        Content = o.Content,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                };
                _appDbContext.ChoiceQuestions.Add((ChoiceQuestion)question);
                break;

            default:
                throw new ArgumentException("Invalid question type");

        }
        await _appDbContext.SaveChangesAsync();
        return question.Id;
    }
}
