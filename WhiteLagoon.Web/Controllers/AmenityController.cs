using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Web.Models.ViewModel;

namespace WhiteLagoon.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var villasNumber = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(villasNumber);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            AmenityVM vm = new()
            {
                VillaList = list
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {
            bool checkUniqueness = _unitOfWork.Amenity.Any(x => x.Id == obj.Amenity.Id);

            if (ModelState.IsValid && !checkUniqueness)
            {
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Villa number already exists!";
            }
            IEnumerable<SelectListItem> list = _unitOfWork.Amenity.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            obj.VillaList = list;
            return View(obj);
        }

        public IActionResult Update(int AmenityId)
        {
            Amenity? obj = _unitOfWork.Amenity.Get(x => x.Id == AmenityId);

            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            AmenityVM vm = new()
            {
                VillaList = list
                ,
                Amenity = obj
            };
            if (obj == null) { return RedirectToAction("Error", "Home"); }
            return View(vm);

        }

        [HttpPost]
        public IActionResult Update(AmenityVM obj)
        {
            if (ModelState.IsValid && obj.Amenity.Id > 0)
            {
                _unitOfWork.Amenity.Update(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<SelectListItem> list = _unitOfWork.Amenity.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            obj.VillaList = list;
            TempData["error"] = "Villa not found!";
            return View(obj);
        }

        public IActionResult Delete(int AmenityId)
        {
            Amenity? obj = _unitOfWork.Amenity.Get(x => x.Id == AmenityId);
            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            AmenityVM vm = new()
            {
                VillaList = list
                ,
                Amenity = obj
            };

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM myObj)
        {
            Amenity? obj = _unitOfWork.Amenity.Get(x => x.Id == myObj.Amenity.Id);


            if (obj is not null)
            {
                _unitOfWork.Amenity.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Amenity is deleted!";
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            AmenityVM vm = new()
            {
                VillaList = list
                ,
                Amenity = obj
            };
            TempData["error"] = "Amenity not found!";
            return View(obj);
        }
    }
}
