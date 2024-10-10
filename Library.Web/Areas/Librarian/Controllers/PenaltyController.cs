using Library.Entities.Models;
using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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