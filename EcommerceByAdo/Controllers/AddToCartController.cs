using EcommerceByAdo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceByAdo.Controllers
{
    public class AddToCartController : Controller
    {
        private readonly IProductATCRepository _productATC;

        public AddToCartController(IProductATCRepository productATC)
        {
            _productATC = productATC;
        }
        public IActionResult Index()
        {
            var result = _productATC.GetAll();
            return View(result);
        }

        public async Task<IActionResult> Cart(Guid id)
        {
            var result = _productATC.add(id);

            return RedirectToAction("Index", "AddToCart");
        }

        public IActionResult Delete(Guid id)
        {
            var result = _productATC.DeleteAll(id);
            return RedirectToAction("Index", "AddToCart");
        }
        
        public IActionResult AddQuanity(Guid id)
        {
            _productATC.AddProductQuantity(id);
            return RedirectToAction("Index", "AddToCart");
        }
        public IActionResult LessQuanity(Guid id)
        {
            _productATC.LessProductQuantity(id);
            return RedirectToAction("Index", "AddToCart");
        }
    }
}
