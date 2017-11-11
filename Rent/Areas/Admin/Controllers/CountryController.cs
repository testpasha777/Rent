using BLL.Interface;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent.Areas.Admin.Controllers
{
    public class CountryController : Controller
    {
        private ICountryService countryService;

        public CountryController(ICountryService _countryService)
        {
            countryService = _countryService;
        }

        // GET: Admin/Country
        public ActionResult Index()
        {
            var countries = countryService.GetAll();
            return View(countries);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CountryCreateViewModel countryVM)
        {
            if(ModelState.IsValid)
            {
                bool createRes = countryService.Add(countryVM);

                if(!createRes)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                    return View(countryVM);
                }

                return RedirectToAction("Index");
            }

            return View(countryVM);
        }

        public ActionResult Details(int id)
        {
            var country = countryService.GetById(id);
            return View(country);
        }

        public ActionResult Edit(int id)
        {
            var country = countryService.GetCountryEditById(id);
            return View(country);
        }

        [HttpPost]
        public ActionResult Edit(CountryEditViewModel countryVM)
        {
            if(ModelState.IsValid)
            {
                bool editConfirtm = countryService.Update(countryVM);

                if(!editConfirtm)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                    return View(countryVM);
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            var country = countryService.GetById(id);
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            countryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}