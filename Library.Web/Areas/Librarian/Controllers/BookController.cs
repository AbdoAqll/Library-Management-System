using Library.Entities.Models;
using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Areas.Librarian.Controllers
{
    [Area(StaticData.LibrarianRole)]
    [Authorize(Roles = StaticData.LibrarianRole)]
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
                        await file.CopyToAsync(fileStream);
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
        public async Task<IActionResult> Update(Book book, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = webHostEnvironment.WebRootPath;

                // Load the existing entity from the database
                var existingBook = await unitOfWork.BookRepository.GetFirstOrDefaultAsync(x => x.Id == book.Id);

                if (existingBook == null)
                {
                    return NotFound();
                }


                existingBook.Title = book.Title;
                existingBook.Description = book.Description;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
                existingBook.Publisher = book.Publisher;
                existingBook.PublishDate = book.PublishDate;
                existingBook.Stock = book.Stock;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(rootPath, @"images\Books");
                    var extension = Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploads, fileName + extension);

                    // Deleting the old image if it exists
                    if (!string.IsNullOrEmpty(existingBook.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(rootPath, existingBook.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    await using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    existingBook.ImageUrl = @"\images\Books\" + fileName + extension;
                }

                await unitOfWork.BookRepository.UpdateAsync(existingBook);
                await unitOfWork.CompleteAsync();
                TempData["Update"] = "Book updated successfully";
                return RedirectToAction("Index");
            }

            return View(book);
        }
    }
}