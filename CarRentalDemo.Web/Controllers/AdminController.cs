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
    public class AdminController : Controller
    {
        private CarRentalDemoDb contextDb = new CarRentalDemoDb();

        [HttpGet]
        public ActionResult Index()
        {
            var activeRents = contextDb.Rents.Include(r => r.Car).Include(r => r.CarRentalUser).Where(r => r.Returned == false);
            return View(activeRents.ToList());
        }

        [HttpGet]
        public ActionResult ReturnCar(int id)
        {
            Rent returnCar = contextDb.Rents.Find(id);
            returnCar.Returned = true;
            contextDb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}