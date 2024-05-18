using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Models.ViewModels;
using BulkyBook.Models.Models;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        //GET
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(objProductList);
        } 
       

      
        //GET
        public IActionResult Upsert(int? Id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ID.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ID.ToString()
                }),

            };

            if(Id == null || Id == 0)
            {
                //Create Product
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get( x=> x.Id == Id );
                return View(productVM);
                //Update Product
            }

        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
           
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if(obj.Product.ImageUrl != null)
                    {
                        var OldImagePath = Path.Combine(wwwRootPath,obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);    
                        }
                    }
                    using(var fileStreams = new FileStream(Path.Combine(uploads,fileName+extension),FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    obj.Product.ImageUrl = @"images\products\" + fileName + extension;
                }
                if(obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);

                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);

                }
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

               
        #region API CALLS
        public IActionResult GetAll()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return Json(new {data = productList});
        }

        //POST
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id)
        {
            var obj = _unitOfWork.Product.Get(x => x.Id == Id); ;
            if (obj == null)
            {
                return Json(new {success=false,message = "Error while deleting"});
            }

            var OldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(OldImagePath))
            {
                System.IO.File.Delete(OldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product deleted successfully" });


        }

        #endregion

    }
}
