using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Library.Web.Areas.Member.Controllers
{
    [Area(StaticData.MemberRole)]
    [Authorize(Roles = StaticData.MemberRole)]
    public class CheckoutController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CheckoutController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var checkouts = await unitOfWork.CheckoutRepository.GetAllAsync(x => x.ApplicationUserId == userId && x.Status != StaticData.InCart, "Book");
            return View(checkouts);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                TempData["Delete"] = "Empty Input!";
                return RedirectToAction("Index");
            }
            var books = await unitOfWork.CheckoutRepository.GetAllAsync(
                x => x.Book.Title.ToLower().Contains(searchString.ToLower()),"Book");


            if (books.IsNullOrEmpty())
            {
                TempData["Delete"] = "Not Found!";
                return RedirectToAction("Index");
            }

            return View("Index", books);
        }

    }
}
