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
    }
}