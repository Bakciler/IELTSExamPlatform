using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.Reading.GET;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.ChoiceQuestions;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.BL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace IELTSExamPlatform.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ReadingController : Controller
    {
        private readonly IReadingService _readingService;

        public ReadingController(IReadingService readingService)
        {
            _readingService = readingService;
        }

        [HttpGet]
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
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var reading = await _readingService.GetByIdAsync(id);
            if (reading == null)
                return NotFound();

            return View(reading); // Details.cshtml səhifəsini göstərəcək
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateReadingDto updatedReading)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _readingService.UpdateReadingAsync(id, updatedReading);
                TempData["SuccessMessage"] = "Reading məlumatları uğurla yadda saxlanıldı!";
                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action(nameof(Index), "Reading", new { area = "Admin" })
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


        [HttpDelete("{passageId:guid}")]
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

        [HttpDelete("{paragraphId:guid}")]
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


        public async Task<IActionResult> QuestionCreateIndex(Guid id)
        {
            //Guid id = Guid.Parse("0199a6c2-c00d-7b59-a23e-9b44c5fbb3a6");

            var passages = await _readingService.GetAllPassagesAsyncByReadingId(id);

            ViewBag.Passages = passages;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionCreateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var questionId = await _readingService.CreateQuestionAsync(request);
                return Ok(new
                {
                    success = true,
                    message = "Question created successfully!",
                    questionId = questionId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
