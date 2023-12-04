using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Description == obj.Name)
            {
                ModelState.AddModelError("", "The description Cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.villas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.villas.FirstOrDefault(x => x.Id == villaId);
            if (obj == null) { return RedirectToAction("Error","Home"); }
            return View(obj);

        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {          
            if (ModelState.IsValid && obj.Id>0) 
            {
                _db.villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa not found!";
            return View();
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
