using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Index(int page = 1)
        {
            var books = await unitOfWork.BookRepository.GetAllAsync();
            var totalBooks = books.Count();
            var pagedBooks = books.Skip((page - 1) * StaticData.pageSize).Take(StaticData.pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling((double)totalBooks / StaticData.pageSize);

            return View(pagedBooks);
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
