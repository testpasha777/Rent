using BLL.Interface;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Create(ApartmentCreateViewModel apartmentCreateVM)
        {

            return View(apartmentCreateVM);
        }
    }
}