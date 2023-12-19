using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.Models.ViewModel;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
  
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var villasNumber = _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");
            return View(villasNumber);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            VillaNumberVM vm = new()
            {
                VillaList = list
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            bool checkUniqueness = _unitOfWork.VillaNumber.Any(x => x.Villa_Number == obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !checkUniqueness)
            {
                _unitOfWork.VillaNumber.Add(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Villa number already exists!";
            }
            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

           obj.VillaList = list;
            return View(obj);
        }

        public IActionResult Update(int Villa_Number)
        {
            VillaNumber? obj = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == Villa_Number);

            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            VillaNumberVM vm = new()
            {
                VillaList = list
                ,VillaNumber=obj
            };
            if (obj == null) { return RedirectToAction("Error", "Home"); }
            return View(vm);

        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM obj)
        {
            if (ModelState.IsValid && obj.VillaNumber.Villa_Number > 0)
            {
                _unitOfWork.VillaNumber.Update(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            obj.VillaList = list;
            TempData["error"] = "Villa not found!";
            return View(obj);
        }

        public IActionResult Delete(int Villa_Number)
        {
            VillaNumber? obj = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == Villa_Number);
            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            VillaNumberVM vm = new()
            {
                VillaList = list
                ,
                VillaNumber = obj
            };

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM myObj)
        {
            VillaNumber? obj = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == myObj.VillaNumber.Villa_Number);
            

            if (obj is not null)
            {
                _unitOfWork.VillaNumber.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Villa Number is deleted!";
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<SelectListItem> list = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            VillaNumberVM vm = new()
            {
                VillaList = list
                ,
                VillaNumber = obj
            };
            TempData["error"] = "Villa Number not found!";
            return View(obj);
        }
    }
}
