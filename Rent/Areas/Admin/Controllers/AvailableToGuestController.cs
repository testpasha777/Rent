﻿using BLL.Interface;
using BLL.Services;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent.Areas.Admin.Controllers
{
    public class AvailableToGuestController : Controller
    {
        private IAvailableToGuestService availableToGuestService;

        public AvailableToGuestController(IAvailableToGuestService _availableToGuest)
        {
            availableToGuestService = _availableToGuest;
        }

        // GET: Admin/AvailableToGuest
        public ActionResult Index()
        {
            var availableToGuests = availableToGuestService.GetAll();
            return View(availableToGuests);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AvailableToGuestCreateViewModel availableToGuestVM)
        {
            if(ModelState.IsValid)
            {
                bool addConfirm = availableToGuestService.Create(availableToGuestVM);

                if(!addConfirm)
                {
                    ModelState.AddModelError("Name", "Name already exists");
                    return View(availableToGuestVM);
                }

                return RedirectToAction("Index");
            }

            return View(availableToGuestVM);
        }

        public ActionResult Details(int id)
        {
            var availableToGuest = availableToGuestService.GetById(id);

            if(availableToGuest == null)
            {
                return HttpNotFound();
            }

            return View(availableToGuest);
        }

        public ActionResult Edit(int id)
        {
            var availableToGuest = availableToGuestService.GetEditById(id);

            if(availableToGuest == null)
            {
                return HttpNotFound();
            }

            return View(availableToGuest);
        }

        [HttpPost]
        public ActionResult Edit(AvailableToGuestEditViewModel availableToGuestEditVM)
        {
            if(ModelState.IsValid)
            {
                var editConfirm = availableToGuestService.Update(availableToGuestEditVM);

                if(!editConfirm)
                {
                    ModelState.AddModelError("Name", "Name alreary exists");
                    return View(availableToGuestEditVM);
                }

                return RedirectToAction("Index");
            }

            return View(availableToGuestEditVM);
        }

        public ActionResult Delete(int id)
        {
            var availableToGuest = availableToGuestService.GetById(id);

            if(availableToGuest == null)
            {
                return HttpNotFound();
            }

            return View(availableToGuest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            availableToGuestService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}