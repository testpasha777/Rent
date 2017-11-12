using BLL.Interface;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent.Areas.Admin.Controllers
{
    public class CityController : Controller
    {
        private ICityService cityService;

        public CityController(ICityService _cityService)
        {
            cityService = _cityService;
        }

        // GET: Admin/City
        public ActionResult Index()
        {
            var cities = cityService.GetAll();
            return View(cities);
        }

        public ActionResult Create()
        {
            var city = cityService.GetCreateCity();
            return View(city);
        }

        [HttpPost]
        public ActionResult Create(CityCreateViewModel cityVM)
        {
            if (ModelState.IsValid)
            {
                bool addConfirm = cityService.Add(cityVM);

                if (!addConfirm)
                {
                    ModelState.AddModelError("Name", "City already exists");
                    return View(cityVM);
                }

                return RedirectToAction("Index");
            }

            return View(cityVM);
        }

        public ActionResult Details(int id)
        {
            var city = cityService.GetById(id);
            return View(city);
        }

        public ActionResult Edit(int id)
        {
            var editCity = cityService.GetEditCityById(id);
            return View(editCity);
        }

        [HttpPost]
        public ActionResult Edit(CityEditViewModel cityVM)
        {
            if (ModelState.IsValid)
            {
                bool editConfirm = cityService.Update(cityVM);

                if (!editConfirm)
                {
                    ModelState.AddModelError("Name", "City already exists");
                    return View(cityVM);
                }

                return RedirectToAction("Index");
            }

            return View(cityVM);
        }

        public ActionResult Delete(int id)
        {
            var city = cityService.GetById(id);
            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            cityService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}