using CarRentalDemo.Web.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using CarRentalDemo.Entities;
using PagedList;
using System.Net;
using CarRentalDemo.Web.Models;


namespace CarRentalDemo.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarsController : Controller
    {
        private CarRentalDemoDb contextDb = new CarRentalDemoDb();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            List<Car> cars = contextDb.Cars.Include(c => c.CarBrand).ToList();

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(cars.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CarsPerBrand(int? id,int? page1)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                CarBrand carBrand = contextDb.CarBrands.Find(id);
                IQueryable<Car> cars = contextDb.Cars.Where(c => c.CarBrendID == id).OrderBy(c=>c.CarBrand.BrendName);

                int pageSize1 = 3;
                int pageNumber1 = (page1 ?? 1);

                SelectedCarsPerBrand selectedCarsPerBrand = new SelectedCarsPerBrand() { SelectedCarBrand = carBrand,
                    SelectedCars = cars.ToPagedList(pageNumber1,pageSize1) };
                return View(selectedCarsPerBrand);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CarBrendID = new SelectList(contextDb.CarBrands, "CarBrendID", "BrendName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarBrendID,CarDescription,CarRegistrationDate,CarImage,PricePerDay,Available")] Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contextDb.Cars.Add(car);
                    contextDb.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.CarBrendID = new SelectList(contextDb.CarBrands, "CarBrendID", "BrendName", car.CarBrendID);
            return View(car);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = contextDb.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarBrendID = new SelectList(contextDb.CarBrands, "CarBrendID", "BrendName", car.CarBrendID);
            return View(car);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var carToUpdate = contextDb.Cars.Find(id);
            if (TryUpdateModel(carToUpdate, "",
                new string[] { "CarBrendID","CarDescription","CarImage","CarRegistrationDate","PricePerDay","Available" }))
            {
                try
                {
                    contextDb.SaveChanges();

                    return RedirectToAction("Index","Admin");
                }
                catch (DataException )
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.CarBrendID = new SelectList(contextDb.CarBrands, "CarBrendID", "BrendName", carToUpdate.CarBrendID);
            return View(carToUpdate);
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
            Car car = contextDb.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Car car = contextDb.Cars.Find(id);
                contextDb.Cars.Remove(car);
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