using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll( x => x.ApplicationUserId == userId,includeProperties :"Product" )    
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderTotal += (cart.Price * cart.Count); 
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            return View();
        }
        public IActionResult Plus(int CartID)
        {
            var CartFromDB = _unitOfWork.ShoppingCart.Get(x => x.Id == CartID);
            CartFromDB.Count += 1;
            _unitOfWork.ShoppingCart.Update(CartFromDB);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        } 
        public IActionResult Minus(int CartID)
        {
            var CartFromDB = _unitOfWork.ShoppingCart.Get(x => x.Id == CartID);
            if(CartFromDB.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(CartFromDB);
            }
            else
            {
                CartFromDB.Count -= 1;
                _unitOfWork.ShoppingCart.Update(CartFromDB);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        } 
        public IActionResult Remove(int CartID)
        {
            var CartFromDB = _unitOfWork.ShoppingCart.Get(x => x.Id == CartID);
            _unitOfWork.ShoppingCart.Remove(CartFromDB);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;
                }
                else
                {
                    return shoppingCart.Product.Price100;
                }
            }
        }
    }
}
