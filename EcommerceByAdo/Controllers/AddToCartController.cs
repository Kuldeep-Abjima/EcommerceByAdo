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

        public ActionResult Delete(Guid id)
        {
            var result = _productATC.DeleteAll(id);
            return RedirectToAction("Index", "AddToCart");
        }
    }
}
