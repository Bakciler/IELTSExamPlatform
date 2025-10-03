using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.Reading.GET;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.ChoiceQuestions;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
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
    public async Task UpdateReadingAsync(Guid readingId, ReadingDto updatedReading)
    {
        var reading = await _appDbContext.Readings
            .Include(r => r.ReadingPassages)
                .ThenInclude(p => p.ReadingParagrahs)
            .FirstOrDefaultAsync(r => r.Id == readingId);

        if (reading == null)
            throw new Exception("Reading not found");

        for (int i = 0; i < updatedReading.ReadingPassages.Count; i++)
        {
            var passageDto = updatedReading.ReadingPassages[i];
            var passage = reading.ReadingPassages.ElementAtOrDefault(i);
            if (passage != null)
            {
                passage.Title = passageDto.Title;
                passage.Description = passageDto.Description;

                for (int j = 0; j < passageDto.ReadingParagrahs.Count; j++)
                {
                    var paraDto = passageDto.ReadingParagrahs[j];
                    var paragraph = passage.ReadingParagrahs.ElementAtOrDefault(j);
                    if (paragraph != null)
                    {
                        paragraph.Key = paraDto.Key;
                        paragraph.Content = paraDto.Content;
                    }
                }
            }
        }

        _appDbContext.Readings.Update(reading);
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

    public async Task<ChoiceQuestion> ChoiceQuestionCreateAsync(ChoiceQuestionCreateDto dto)
    {
        var choiceQuestion = new ChoiceQuestion
        {
            Id = Guid.NewGuid(),
            ReadingPassageId = dto.ReadingPassageId,
            QuestionText = dto.QuestionText,
            Order = dto.Order,
            QuestionOptions = dto.Options.Select(o => new QuestionOption
            {
                Id = Guid.NewGuid(),
                Code = o.Code,
                Content = o.Content,
                IsCorrect = o.IsCorrect
            }).ToList()
        };

        await _appDbContext.ChoiceQuestions.AddAsync(choiceQuestion);
        await _appDbContext.SaveChangesAsync();

        return choiceQuestion;
    }
    public async Task<ReadingPassageDto> AddPassageAsync(Guid readingId,CreateReadingPassageDto dto)
    {
        var reading = await _appDbContext.Readings
            .Include(r => r.ReadingPassages)
            .FirstOrDefaultAsync(r => r.Id == readingId);

        if (reading == null)
            throw new Exception("Reading not found");

        if (reading.ReadingPassages.Count >= 3)
            throw new Exception("A reading can have maximum 3 passages.");


        var newPassage = new ReadingPassage
        {
            Title = dto.Title,
            Description = dto.Description,
            ReadingParagrahs = new List<ReadingParagraphs>()
        };

        reading.ReadingPassages.Add(newPassage);
        await _appDbContext.SaveChangesAsync();

        return new ReadingPassageDto
        {
            Id = newPassage.Id,
            Title = newPassage.Title,
            Description = newPassage.Description,
            ReadingParagrahs = new List<ReadingParagraphDto>()
        };
    }

    public async Task<ReadingParagraphDto> AddParagraphAsync(Guid passageId,CreateReadingParagraphsDto dto)
    {
        var passage = await _appDbContext.ReadingPassages
            .Include(p => p.ReadingParagrahs)
            .FirstOrDefaultAsync(p => p.Id == passageId);

        if (passage == null)
            throw new Exception("Passage not found");

        var newParagraph = new ReadingParagraphs
        {
            Key = dto.Key,
            Content = dto.Content
        };

        passage.ReadingParagrahs.Add(newParagraph);
        await _appDbContext.SaveChangesAsync();

        return new ReadingParagraphDto
        {
            Id = newParagraph.Id,
            Key = newParagraph.Key,
            Content = newParagraph.Content
        };
    }
}
