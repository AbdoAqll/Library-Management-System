using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Web.Areas.Librarian.Controllers
{
    [Area(StaticData.LibrarianRole)]
    [Authorize(Roles = StaticData.LibrarianRole)]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIdentity =(ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string userId = claim.Value;
            var users = await unitOfWork.ApplicationUserRepository.GetAllAsync(x => x.Id != userId);
            return View(users);
        }

        public async Task<IActionResult> LockUnlock(string id)
        {
            var user = await unitOfWork.ApplicationUserRepository.GetFirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            if(user.LockoutEnd!=null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
                TempData["Create"] = "User Unlocked Successfully";
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
                TempData["Delete"] = "User Locked Successfully";
            }
            await unitOfWork.CompleteAsync();
            return RedirectToAction("Index" , "Users" , new {area = StaticData.LibrarianRole});

        }
    }
}
