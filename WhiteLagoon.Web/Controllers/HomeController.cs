using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Web.Models;
using WhiteLagoon.Web.Models.ViewModel;

namespace WhiteLagoon.Web.Controllers
{
	public class HomeController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
		{
			HomeVM homeVM = new HomeVM()
			{
				VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity")
				,Nights=1
				,CheckInDate= DateOnly.FromDateTime(DateTime.Now)
			};
			return View(homeVM);
		}
		[HttpPost]
        public IActionResult GetVillasByDate(int nights, DateOnly checkInDate)
        {

            var villaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity");
            foreach (var item in villaList)
            {
                if (item.Id % 2 == 0)
                {
                    item.IsAvailable = false;
                }
            }
			HomeVM homeVM = new HomeVM() { 
			CheckInDate= checkInDate,
			VillaList = villaList,
			Nights = nights
			};

            return PartialView("_VillaList",homeVM);
        }
        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View();
		}
	}
}