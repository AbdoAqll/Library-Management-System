using Library.Entities.Models;
using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library.Web.Areas.Librarian.Controllers
{
    [Area(StaticData.LibrarianRole)]
    [Authorize(Roles = StaticData.LibrarianRole)]
    public class ReturnController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ReturnController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ApprovedByAdminCheckOuts = await unitOfWork.CheckoutRepository.GetAllAsync
                (c => c.Status == StaticData.ApprovedByAdmin, "Book,ApplicationUser");

            return View(ApprovedByAdminCheckOuts);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var checkout = await unitOfWork.CheckoutRepository.GetFirstOrDefaultAsync(x => x.Id == id, "Book,ApplicationUser");
            var book = checkout.Book;
            Return ret = new Return()
            {
                CheckoutId = id,
                ReturnDate = DateTime.Now,
                HasPenalty = DateTime.Now > checkout.DueDate,
            };
            book.Stock++;
            checkout.Status = StaticData.Returned;
            await unitOfWork.ReturnRepository.AddAsync(ret);
            await unitOfWork.CompleteAsync();
            TempData["Success"] = "Book Returned";
            if (ret.HasPenalty)
            {
                var daysDiff = (ret.ReturnDate - checkout.DueDate)?.Days;

                var pen = new Penalty()
                {
                    Amount = ((decimal)daysDiff!) * StaticData.PenaltyPerDay,
                    Return = ret,
                };
                await unitOfWork.PenaltyRepository.AddAsync(pen);
                await unitOfWork.CompleteAsync();
                return View("BookPenalty", pen);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? username)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(username))
            {
                TempData["Delete"] = "Empty Input!";
                return RedirectToAction("Index");
            }
            var checkouts = await unitOfWork.ReturnRepository.GetAllReturnsFilterdByUsernnameAsync(username);

            if (checkouts.IsNullOrEmpty())
            {
                TempData["Delete"] = "Not Found!";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", checkouts);
        }
    }
}