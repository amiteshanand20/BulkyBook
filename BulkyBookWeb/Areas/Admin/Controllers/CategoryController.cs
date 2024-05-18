using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET
        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoryList = _unitOfWork.Category.GetAll();
            return View(ObjCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder should not be same as name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        } 

        //GET
        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            //var DataFromDb = _db.categories.Find(Id);
            var DataFromDb = _unitOfWork.Category.Get(x => x.ID==Id);

            if(DataFromDb == null)
            {
                return NotFound();
            }
            return View(DataFromDb);
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder should not be same as name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            var DataFromDb = _unitOfWork.Category.Get(x => x.ID == Id); ;

            if(DataFromDb == null)
            {
                return NotFound();
            }
            return View(DataFromDb);
        }
        
        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Id)
        {
            var obj = _unitOfWork.Category.Get(x => x.ID == Id); ;
            if(obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }
    }
}
