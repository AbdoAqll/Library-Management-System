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
    public class PenaltyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PenaltyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var allPenalties = await unitOfWork.PenaltyRepository.GetAllPenalties();
            return View(allPenalties);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? username)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(username))
            {
                TempData["Delete"] = "Empty Input!";
                return RedirectToAction("Index");
            }
            var penalties = await unitOfWork.PenaltyRepository.GetAllPenaltiesFilterdByUsernnameAsync(username);

            if (penalties.IsNullOrEmpty())
            {
                TempData["Delete"] = "Not Found!";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", penalties);
        }

        // Obselete
        //public async Task<IActionResult> PayPenalty(int id)// pen id
        //{
        //    var pen = await unitOfWork.PenaltyRepository.GetFirstPenalty(id);
        //    var ret = pen.Return;
        //    var checkout = pen.Return.CheckOut;
        //    var book = pen.Return.CheckOut.Book;
        //    book.Stock++;
        //    checkout.Status = StaticData.Returned;
        //    await unitOfWork.ReturnRepository.AddAsync(ret);
        //    await unitOfWork.CompleteAsync();
        //    TempData["Success"] = "Book Returned";
        //    return RedirectToAction("Index");
        //}
    }
}