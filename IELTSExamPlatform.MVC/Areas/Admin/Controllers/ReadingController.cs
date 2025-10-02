using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.Reading.GET;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.BL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace IELTSExamPlatform.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReadingController : Controller
    {
        private readonly IReadingService _readingService;

        public ReadingController(IReadingService readingService)
        {
            _readingService = readingService;
        }
        public async Task<IActionResult> Index()
        {
            var readings = await _readingService.GetAllAsync();
            return View(readings);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReadingDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _readingService.CreateAsyncReading(dto);

            return RedirectToAction("Index", new { created = 1 });
        }

        public IActionResult QuestionsIndex()
        {
            return View();
        }


        public IActionResult AddFillInTheBlankQuestion()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddFillInTheBlankQuestion(CreateFillInTheBlankDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _readingService.AddFillInTheBlankQuestion(dto);

            return RedirectToAction("QuestionsIndex");
        }

        // GET: Admin/Reading/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var reading = await _readingService.GetByIdAsync(id);
            if (reading == null)
                return NotFound();

            return View(reading); // Details.cshtml səhifəsini göstərəcək
        }

        // POST: Admin/Reading/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(Guid id, [FromBody] ReadingDto updatedReading)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _readingService.UpdateReadingAsync(id, updatedReading);
                return Ok(new { success = true, message = "Reading updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePassage(Guid passageId)
        {
            try
            {
                await _readingService.DeletePassageAsync(passageId);
                return Ok(new { success = true, message = "Passage deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteParagraph(Guid paragraphId)
        {
            try
            {
                await _readingService.DeleteParagraphAsync(paragraphId);
                return Ok(new { success = true, message = "Paragraph deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


    }
}
