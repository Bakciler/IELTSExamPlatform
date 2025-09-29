using IELTSExamPlatform.BL.DTOs.Question;
using IELTSExamPlatform.BL.DTOs.Reading;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.FillBlanks;
using IELTSExamPlatform.BL.DTOs.ReadingQuestions.Get;
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

        // 🔹 Mövcud metod: yalnız FillInTheBlank sualları
        public async Task<IActionResult> ByPassage(Guid passageId)
        {
            var data = await _readingService.GetQuestionsAsync(passageId);
            return View(data);
        }

        // 🔹 Yeni metod: Bütün sual tiplərini gətirir
        public async Task<IActionResult> AllQuestions(Guid passageId)
        {
            var data = await _readingService.GetAllQuestionsByPassageAsync(passageId);
            return View(data);
        }

        // 🔹 Yeni metod: Ümumi sual yaratmaq (Boolean, Choice, FillInTheBlank, MatchHeading)
        public IActionResult CreateQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _readingService.CreateQuestionAsync(dto);

            return RedirectToAction("QuestionsIndex");
        }
    }
}