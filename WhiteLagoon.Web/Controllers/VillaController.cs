using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    [Authorize]
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
       
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
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
                if(obj.Image != null)
                {
                    string filName = Guid.NewGuid().ToString()+ Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImages");

                    using (var fileStream= new FileStream(Path.Combine(imagePath, filName), FileMode.Create))
                    {
                        obj.Image.CopyTo(fileStream);
                    }
                    obj.ImageURL = @"\images\VillaImages\" + filName;
                }
                else
                {
                    obj.ImageURL = "http://via.placeholder.com/600X400";
                }
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(x => x.Id == villaId);
            if (obj == null) { return RedirectToAction("Error","Home"); }
            return View(obj);

        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {          
            if (ModelState.IsValid && obj.Id>0) 
            {
                if (obj.Image != null)
                {
                    if (!string.IsNullOrEmpty(obj.ImageURL))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    string filName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImages");

                    using (var fileStream = new FileStream(Path.Combine(imagePath, filName), FileMode.Create))
                    {
                        obj.Image.CopyTo(fileStream);
                    }
                    obj.ImageURL = @"\images\VillaImages\" + filName;
                }
              
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa not found!";
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(x => x.Id == villaId);
            if (obj is null)
            {
              
                return RedirectToAction("Error", "Home");
            }
       
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa myObj)
        {
            Villa? obj = _unitOfWork.Villa.Get(x => x.Id == myObj.Id);
            if (obj is not null)
            {
                if (!string.IsNullOrEmpty(obj.ImageURL))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageURL.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.Villa.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Action Completed Successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Villa not found!";
            return View(obj);
        }
    }
}
