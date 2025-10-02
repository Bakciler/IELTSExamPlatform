using IELTSExamPlatform.BL.DTOs.Reading;
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
    }
}
