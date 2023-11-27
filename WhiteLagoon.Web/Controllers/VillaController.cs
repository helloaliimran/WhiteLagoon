using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
	public class VillaController : Controller
	{
		private ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
			_db = db;           
        }
        public IActionResult Index()
		{
			var villas = _db.villas.ToList();
			return View(villas);
		}
	}
}
