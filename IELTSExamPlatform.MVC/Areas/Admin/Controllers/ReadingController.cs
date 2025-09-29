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
        public IActionResult Index()
        {
            return View();
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

            return RedirectToAction("Index");
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


        public async Task<IActionResult> ByPassage()
        {
            Guid xaqan = Guid.Parse("019992d6-db65-708a-a52a-a028c8df014e");
            var data = await _readingService.GetQuestionsAsync(xaqan);

            return View(data);
        }
    }
}
