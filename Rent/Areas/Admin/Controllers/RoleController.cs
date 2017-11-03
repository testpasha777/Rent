using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.ViewModel;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BLL.Infrastructure.Identity;
using DAL.Entities.Identity;

namespace Rent.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationRoleManager roleMnager;

        public RoleController(ApplicationRoleManager _role)
        {
            roleMnager = _role;
        }

        // GET: Admin/Role
        public ActionResult Index()
        {
            var roles = roleMnager.Roles.Select(i => new RoleViewModel
            {
                Id = i.Id,
                Name = i.Name
            });

            return View(roles);
        }

        //GET: Admin/Role/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = roleMnager.FindById(id);
            RoleViewModel roleVM = new RoleViewModel();
            roleVM.Id = role.Id;
            roleVM.Name = role.Name;

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(roleVM);
        }

        // GET: Admin/Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                roleMnager.Create(new IdentityRole(roleViewModel.Name));
                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        // GET: Admin/Role/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = roleMnager.FindById(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            RoleViewModel roleVM = new RoleViewModel();
            roleVM.Id = role.Id;
            roleVM.Name = role.Name;

            return View(roleVM);
        }

        // POST: Admin/Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = roleMnager.FindById(roleViewModel.Id);
                role.Name = roleViewModel.Name;
                roleMnager.Update(role);
                
                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        // GET: Admin/Role/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = roleMnager.FindById(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            RoleViewModel roleVM = new RoleViewModel();
            roleVM.Id = role.Id;
            roleVM.Name = role.Name;

            return View(roleVM);
        }

        // POST: Admin/Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var role = roleMnager.FindById(id);
            roleMnager.Delete(role);

            return RedirectToAction("Index");
        }
    }
}
