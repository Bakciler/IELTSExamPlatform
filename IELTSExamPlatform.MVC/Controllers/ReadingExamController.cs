using IELTSExamPlatform.BL.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IELTSExamPlatform.MVC.Controllers
{
    public class ReadingExamController : Controller
    {
        private readonly IReadingExamService _readingExamService;
        public ReadingExamController(IReadingExamService readingExamService)
        {
            _readingExamService = readingExamService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var exam = await _readingExamService.RandomReadingExam();
            if(exam == null)
                return NotFound(); 
            
            return View(exam);
        }
    }
}
