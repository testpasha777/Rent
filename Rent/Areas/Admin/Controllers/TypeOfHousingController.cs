using BLL.Interface;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent.Areas.Admin.Controllers
{
    public class TypeOfHousingController : Controller
    {
        private ITypeOfHousingService typeOfHousingService;

        public TypeOfHousingController(ITypeOfHousingService _typeOfHousingService)
        {
            typeOfHousingService = _typeOfHousingService;
        }

        // GET: Admin/TypeOfHousing
        public ActionResult Index()
        {
            var typesOfHousing = typeOfHousingService.GetAll();
            return View(typesOfHousing);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TypeOfHousingCreateViewModel typeOfHousingVM)
        {
            if(ModelState.IsValid)
            {
                bool addConfirm = typeOfHousingService.Create(typeOfHousingVM);

                if(!addConfirm)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                    return View(typeOfHousingVM);
                }

                return RedirectToAction("Index");
            }

            return View(typeOfHousingVM);
        }

        public ActionResult Details(int id)
        {
            var typeOfHousing = typeOfHousingService.GetById(id);
            return View(typeOfHousing);
        }

        public ActionResult Edit(int id)
        {
            var typeOfHousing = typeOfHousingService.GetEditById(id);
            return View(typeOfHousing);
        }

        [HttpPost]
        public ActionResult Edit(TypeOfHousingEditViewModel typeOfHousingVM)
        {
            if(ModelState.IsValid)
            {
                var editConfirm = typeOfHousingService.Update(typeOfHousingVM);

                if(!editConfirm)
                {
                    ModelState.AddModelError("Name", "Name alreadt exists");
                    return View(typeOfHousingVM);
                }

                return RedirectToAction("Index");
            }

            return View(typeOfHousingVM);
        }

        public ActionResult Delete(int id)
        {
            var typeOfHousing = typeOfHousingService.GetById(id);
            return View(typeOfHousing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            typeOfHousingService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}