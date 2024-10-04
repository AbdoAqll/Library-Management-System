using Library.DataAccess.RepositoryImplementation;
using Library.Entities.Models;
using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Web.Areas.Member.Controllers
{
    [Area(StaticData.MemberRole)]
    [Authorize(Roles = StaticData.MemberRole)]
    public class BorrowController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        public BorrowController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            var inCartCheckouts = await unitOfWork.CheckoutRepository.GetAllAsync(x => x.ApplicationUserId == userId && x.Status == StaticData.InCart, "Book");

            return View(inCartCheckouts);
        }

        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            var checkout = await unitOfWork.CheckoutRepository.GetFirstOrDefaultAsync(x => x.Id == id) ;

            checkout.Status = StaticData.ConfirmedByUser;

            await unitOfWork.CompleteAsync();
            TempData["Update"] = "Done!";

            return RedirectToAction("Index");
        } 
        [HttpGet]
        public async Task<IActionResult> Borrow(int id)
        {
            if (ModelState.IsValid) {
                var book = await unitOfWork.BookRepository.GetFirstOrDefaultAsync(x => x.Id == id);
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var userId = claim.Value;


                // User can only borrow a book again in case it is returned or the request of the book was disaproved by a librarian.
                var checkRepeatedBooks = await unitOfWork.CheckoutRepository.
                    GetFirstOrDefaultAsync(x => x.BookId == id && x.ApplicationUserId == userId &&
                    (x.Status == StaticData.DisaprrovedByAdmin || x.Status == StaticData.Returned));

                if(checkRepeatedBooks != null)
                {
                    TempData["Delete"] = "You already have this book in your borrowing list";
                    return RedirectToAction("Index", "Home", new { area = StaticData.MemberRole });
                }

                var checkout = new Checkout() {
                    BookId = book.Id,
                    ApplicationUserId = userId,
                    DueDate = null,
                    CheckoutDate = null,
                    Status = StaticData.InCart
                };

                await unitOfWork.CheckoutRepository.AddAsync(checkout);
                await unitOfWork.CompleteAsync();


            }
            TempData["Create"] = "Book is sent to your borrowing list succesfully";

            return RedirectToAction("Index", "Home", new { area = StaticData.MemberRole });
        } 
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var toBeDeletedCheckout = await unitOfWork.CheckoutRepository.GetFirstOrDefaultAsync(x=> x.Id == id);
            await unitOfWork.CheckoutRepository.RemoveAsync(toBeDeletedCheckout);
            await unitOfWork.CompleteAsync();
            TempData["Delete"] = "Book is removed from your borrowing list succesfully";
            return RedirectToAction("Index");
        }

    }
}
