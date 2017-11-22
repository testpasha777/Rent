using BLL.Interface;
using BLL.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rent.Controllers
{
    public class ApartmentController : Controller
    {
        private IApartmentService apartmentService;

        public ApartmentController(IApartmentService _apartmentService)
        {
            apartmentService = _apartmentService;
        }

        // GET: Apartment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ApartmentCreateViewModel apartmentCreateVM = apartmentService.GetCreateApartment();
            return View(apartmentCreateVM);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ApartmentCreateViewModel apartmentCreateVM)
        {
            if(ModelState.IsValid)
            {
                if(apartmentCreateVM.images.Length == 1 && apartmentCreateVM.images[0] == null)
                {
                    ModelState.AddModelError("Images", "Add minimum 1 photo");
                    return View(apartmentCreateVM);
                }

                bool addConfirm = await apartmentService.Create(apartmentCreateVM, User.Identity.GetUserId(), User.Identity.Name);

                if (!addConfirm)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                    return View(apartmentCreateVM);
                }

                return RedirectToAction("Index", "Home");
            }


            return View(apartmentCreateVM);
        }
    }
}