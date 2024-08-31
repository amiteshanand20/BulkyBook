using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Models.ViewModels;
using BulkyBook.Models.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        } 
       

      
        //GET
        public IActionResult Upsert(int? Id)
        {
           
            if(Id == null || Id == 0)
            {
                //Create Company
                return View(new Company());
            }
            else
            {
                Company companyObj = _unitOfWork.Company.Get( x=> x.Id == Id );
                return View(companyObj);
            }

        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company companyObj)
        {
           
            if (ModelState.IsValid)
            {
                if(companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);

                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);

                }
                _unitOfWork.Save();
                TempData["Success"] = "Company created successfully";
                return RedirectToAction("Index");
            }

            return View(companyObj);
        }

               
        #region API CALLS
        public IActionResult GetAll()
        {
            List<Company> CompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data = CompanyList});
        }

        //POST
        [HttpDelete]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id)
        {
            var obj = _unitOfWork.Company.Get(x => x.Id == Id); ;
            if (obj == null)
            {
                return Json(new {success=false,message = "Error while deleting"});
            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Company deleted successfully" });


        }

        #endregion

    }
}
