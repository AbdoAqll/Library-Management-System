using Library.Entities.Models;
using Library.Entities.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var books = await unitOfWork.BookRepository.GetAllAsync();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await unitOfWork.BookRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            return View(book);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var book = await unitOfWork.BookRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            // deleting the book image from wwwroot
            var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, book.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            await unitOfWork.BookRepository.RemoveAsync(book);
            await unitOfWork.CompleteAsync();
            TempData["Delete"] = "Book deleted successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(rootPath, @"images\Books");
                    var extension = Path.GetExtension(file.FileName);
                    await using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    book.ImageUrl = @"\images\Books\" + fileName + extension;
                }
                await unitOfWork.BookRepository.AddAsync(book);
                await unitOfWork.CompleteAsync();
                TempData["Create"] = "Book created successfully";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var book = await unitOfWork.BookRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(rootPath, @"images\Books");
                    var extension = Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploads, fileName + extension);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    book.ImageUrl = @"\images\Books\" + fileName + extension;
                    await unitOfWork.BookRepository.UpdateAsync(book);
                    await unitOfWork.CompleteAsync();
                    TempData["Update"] = "Book Updated successfully";

                }

                return RedirectToAction("Index");
            }

            return View(book);
        }
    }
}