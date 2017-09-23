using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentalDemo.Entities;
using CarRentalDemo.Web.DataContexts;
using PagedList;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace CarRentalDemo.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CarBrandsController : Controller
    {
        private CarRentalDemoDb contextDb = new CarRentalDemoDb();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;
            
            IQueryable<CarBrand> carBrands = contextDb.CarBrands.OrderBy(x => x.BrendName);

            if (!string.IsNullOrEmpty(searchString))
            {
                carBrands = carBrands.Where(s => s.BrendName.Contains(searchString));
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(carBrands.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="BrendName,BrendImage")] CarBrand carBrand)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contextDb.CarBrands.Add(carBrand);
                    contextDb.SaveChanges();
                    return RedirectToAction("Index","Admin");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(carBrand);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBrand carBrend = contextDb.CarBrands.Find(id);
            if(carBrend == null)
            {
                return HttpNotFound();
            }
            return View(carBrend);
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CarBrandsToUpdagte = contextDb.CarBrands.Find(id);
            if(TryUpdateModel(CarBrandsToUpdagte,"",
                new string[] {"BrendName", "BrendImage" }))
            {
                try
                {
                    contextDb.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            CarBrand carBrend = contextDb.CarBrands.Find(id);
            if (carBrend == null)
            {
                return HttpNotFound();
            }
            return View(carBrend);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CarBrand carBrend = contextDb.CarBrands.Find(id);
                contextDb.CarBrands.Remove(carBrend);
                contextDb.SaveChanges();
            }
            catch(DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index","Admin");
        }

        protected override void Dispose(bool disposing)
        {
            contextDb.Dispose();
            base.Dispose(disposing);
        }
    }
}