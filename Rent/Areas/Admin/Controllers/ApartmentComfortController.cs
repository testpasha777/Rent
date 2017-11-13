using BLL.Interface;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent.Areas.Admin.Controllers
{
    public class ApartmentComfortController : Controller
    {
        private IApartmentComfortService apartmentComfortService;

        public ApartmentComfortController(IApartmentComfortService _apartmentComfortService)
        {
            apartmentComfortService = _apartmentComfortService;
        }

        // GET: Admin/ApartmentComfort
        public ActionResult Index()
        {
            var apartmentComforts = apartmentComfortService.GetAll();
            return View(apartmentComforts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ApartmentComfortCreateViewModel apartmentComfortVM)
        {
            if(ModelState.IsValid)
            {
                bool addConfirm = apartmentComfortService.Create(apartmentComfortVM);

                if(!addConfirm)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                    return View(apartmentComfortVM);
                }

                return RedirectToAction("Index");
            }

            return View(apartmentComfortVM);
        }

        public ActionResult Details(int id)
        {
            var apartmentComfort = apartmentComfortService.GetById(id);
            return View(apartmentComfort);
        }

        public ActionResult Edit(int id)
        {
            var apartmentComfortEidt = apartmentComfortService.GetEditById(id);
            return View(apartmentComfortEidt);
        }

        [HttpPost]
        public ActionResult Edit(ApartmentComfortEditViewModel apartmentComfortVM)
        {
            if(ModelState.IsValid)
            {
                var editConfirm = apartmentComfortService.Update(apartmentComfortVM);

                if(!editConfirm)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                    return View(apartmentComfortVM);
                }

                return RedirectToAction("Index");
            }

            return View(apartmentComfortVM);
        }

        public ActionResult Delete(int id)
        {
            var apartmentComfort = apartmentComfortService.GetById(id);
            return View(apartmentComfort);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            apartmentComfortService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}