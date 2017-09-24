using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentalDemo.Web.DataContexts;
using CarRentalDemo.Entities;
using CarRentalDemo.Web.Models;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarRentalDemo.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RentsController : Controller
    {
        private CarRentalDemoDb contextDb = new CarRentalDemoDb();

        [HttpGet]
        public ActionResult SelectedCar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car selectedCar = contextDb.Cars.Find(id);
            Rent rentCar = new Rent() { CarID = selectedCar.CarID };
            rentCar.CreateDate = DateTime.Now;

            SelectedCarForRentModel selectedCarForRent = new SelectedCarForRentModel() { SelectedCar = selectedCar, RentedCar = rentCar };
            return View(selectedCarForRent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectedCar([Bind(Include = "CarID,DateOfRent,DateOfReturn")] Rent RentedCar)
        {
            Car selectedCar = contextDb.Cars.Find(RentedCar.CarID);
            TempData["RentedCar"] = RentedCar;
      
            var validationResults = RentedCar.Validate(new ValidationContext(RentedCar, null, null));

            foreach (var error in validationResults)
                foreach (var memberName in error.MemberNames)
                    ModelState.AddModelError("RentedCar." + memberName, error.ErrorMessage);

            if (ModelState.IsValidField("RentedCar.DateOfRent") && ModelState.IsValidField("RentedCar.DateOfReturn"))
            {
                return RedirectToAction("RentCar");
            }
            SelectedCarForRentModel selectedCarForRent = new SelectedCarForRentModel() { SelectedCar = selectedCar, RentedCar = RentedCar };
            return View(selectedCarForRent);
            
        }

        [HttpGet]
        public ActionResult RentCar()
        {
            Rent rentCar = TempData["RentedCar"] as Rent;
            Car selectedCar = contextDb.Cars.Find(rentCar.CarID);
            decimal days = (rentCar.DateOfReturn - rentCar.DateOfRent).Value.Days;
            rentCar.Price = days * selectedCar.PricePerDay;

            SelectedCarForRentModel selectedCarForRent = new SelectedCarForRentModel() { SelectedCar = selectedCar, RentedCar = rentCar };

            return View(selectedCarForRent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RentCar([Bind(Include = "CarID,DateOfRent,DateOfReturn,PayMetod,Price")] Rent RentedCar)
        {
            RentedCar.Returned = false;
            var userce = User.Identity.GetUserId();
            RentedCar.UserID = userce;
            Car SelectedCar = contextDb.Cars.Find(RentedCar.CarID);
            SelectedCar.Available = false;
            if (ModelState.IsValid)
            {
                contextDb.Rents.Add(RentedCar);
                contextDb.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}