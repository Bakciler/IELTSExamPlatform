using IELTSExamPlatform.BL.DTOs.Reading.GET;
using IELTSExamPlatform.BL.DTOs.ReadingExam;
using IELTSExamPlatform.BL.Services.Abstractions;
using IELTSExamPlatform.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace IELTSExamPlatform.BL.Services.Implements;
public class ReadingExamService : IReadingExamService
{
    private readonly AppDbContext _context;
    public ReadingExamService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<GetReadingExamDto> RandomReadingExam()
    {
        var readingId = Guid.Parse("0199a6c2-c00d-7b59-a23e-9b44c5fbb3a6");

        var reading = await _context.Readings
            .Include(r => r.ReadingPassages)
                .ThenInclude(p => p.ReadingParagrahs)
            .FirstOrDefaultAsync(r => r.Id == readingId);

        if (reading == null)
            throw new Exception("No data found");

        var dto = new GetReadingExamDto
        {
            Id = reading.Id,
            readingPassageDtos = reading.ReadingPassages.Select(p => new ReadingPassageDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,

                // ✅ Hər passage üçün paragraph-ları daxil et
                ReadingParagrahs = p.ReadingParagrahs.Select(par => new ReadingParagraphDto
                {
                    Id = par.Id,
                    Key = par.Key,
                    Content = par.Content
                }).ToList()
            }).ToList()
        };

        return dto;
    }


}
