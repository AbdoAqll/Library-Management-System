using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Areas.Member.Controllers
{
    [Area(StaticData.MemberRole)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var books = await unitOfWork.BookRepository.GetAllAsync();
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await unitOfWork.BookRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? searchString)
        {
            if(string.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index");
            }
            var books = await unitOfWork.BookRepository.SearchBooksAsync(searchString);
            return View("Index", books);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
