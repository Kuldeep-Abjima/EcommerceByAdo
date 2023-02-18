using EcommerceByAdo.Interfaces;
using EcommerceByAdo.Models;
using EcommerceByAdo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using EcommerceByAdo.Services;
using System.Collections.Generic;

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

        public async Task<IActionResult> Detail(long id)
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
    }
}
