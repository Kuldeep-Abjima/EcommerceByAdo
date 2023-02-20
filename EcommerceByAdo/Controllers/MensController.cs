using EcommerceByAdo.Interfaces;
using EcommerceByAdo.Models;
using EcommerceByAdo.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceByAdo.Controllers
{
    public class MensController : Controller
    {
        private readonly IMensRepository _mensRepository;
        private readonly IPhotoServices _photoServices;

        public MensController(IMensRepository mensRepository, IPhotoServices photoServics)
        {
            _mensRepository = mensRepository;
            _photoServices = photoServics;
        }
        public IActionResult Index()
        {
            List<Mens> mens = _mensRepository.GetAll();
            return View(mens);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var men = _mensRepository.getbyId(id);
            return View(men);
            
        
        }


         public IActionResult Create()
         {
            CreateMensViewModel cmvm = new CreateMensViewModel();
            return View(cmvm);

         }


        [HttpPost]

        public async Task<IActionResult> Create(CreateMensViewModel cmvm)
        {
            if (ModelState.IsValid)
            {
                var img = await _photoServices.AddPhotoAsync(cmvm.CImage);
                var mens = new Mens()
                {
                    CName = cmvm.CName,
                    CImage = img.Url.ToString(),
                    Rate = cmvm.Rate,
                    Category = cmvm.Category,
                };
                _mensRepository.add(mens);

                return RedirectToAction("Index", "Mens");
            }
            return View(cmvm);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var men = _mensRepository.getbyId(id);
            if(men == null)
            {
                return View("Error");
            }
            var menEdit = new EditMenViewModel()
            {
                CName = men.CName,
                Rate = men.Rate,
                Category = men.Category,
                URL = men.CImage
            };
            return View(menEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditMenViewModel editMenView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit mens cloth");
                return View("Edit", editMenView);
            }
            var men = _mensRepository.getbyId(id);
            if (men != null)
            {
                try
                {
                    await _photoServices.DeletePhotoAsync(men.CImage);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "could not delete photo");
                    return View(editMenView);
                }

                var photoResult = await _photoServices.AddPhotoAsync(editMenView.CImage);

                var mens = new Mens
                {
                    Identifier = id,
                    CName = editMenView.CName,
                    Rate = editMenView.Rate,
                    CImage = photoResult.Url.ToString(),
                    Category = editMenView.Category,

                };
                _mensRepository.UpdatebyId(mens);
                return RedirectToAction("Index", "Mens");
            }
            else
            {
                return View(editMenView);
            }
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var men = _mensRepository.getbyId(id);
            if(men == null)
            {
                return View("Error");
            }
            return View(men);
        }




        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteMen(Guid id)
        {
            var men = _mensRepository.getbyId(id);
            if (men == null)
            {
                return View("Error");
            }
            var delete = _mensRepository.DeletebyId(men.Identifier);
            return RedirectToAction("Index", "Mens");

        }
    }
}
