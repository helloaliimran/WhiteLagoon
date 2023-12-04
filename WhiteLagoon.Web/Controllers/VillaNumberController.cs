using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.Models.ViewModel;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villasNumber = _db.VillaNumbers.Include(x => x.Villa).ToList();
            return View(villasNumber);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> list = _db.villas.ToList().Select(u => new SelectListItem
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
            bool checkUniqueness = _db.VillaNumbers.Any(x => x.Villa_Number == obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !checkUniqueness)
            {
                _db.VillaNumbers.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Villa number already exists!";
            }
            IEnumerable<SelectListItem> list = _db.villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

           obj.VillaList = list;
            return View(obj);
        }

        public IActionResult Update(int Villa_Number)
        {
            VillaNumber? obj = _db.VillaNumbers.FirstOrDefault(x => x.Villa_Number == Villa_Number);
            IEnumerable<SelectListItem> list = _db.villas.ToList().Select(u => new SelectListItem
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
                _db.VillaNumbers.Update(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction("Index");
            }
            IEnumerable<SelectListItem> list = _db.villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            obj.VillaList = list;
            TempData["error"] = "Villa not found!";
            return View(obj);
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.villas.FirstOrDefault(x => x.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa myObj)
        {
            Villa? obj = _db.villas.FirstOrDefault(x => x.Id == myObj.Id);
            if (obj is not null)
            {
                _db.villas.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa not found!";
            return View(obj);
        }
    }
}
