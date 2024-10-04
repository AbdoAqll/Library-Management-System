using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            var checkouts = await unitOfWork.CheckoutRepository.GetAllAsync(x => x.ApplicationUserId == userId, "Book");
            return View(checkouts);
        }

    }
}
