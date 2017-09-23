using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentalDemo.Entities;
using CarRentalDemo.Web.DataContexts;
using CarRentalDemo.Web.Models;
using PagedList;

namespace CarRentalDemo.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private CarRentalDemoDb contextDb = new CarRentalDemoDb();

        [HttpGet]
        public ActionResult Index(string currentFilter, string searchString, int? page,int? page1)
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

            IQueryable<Car> car = contextDb.Cars.OrderBy(x => x.CarBrand.BrendName);

            if (!string.IsNullOrEmpty(searchString))
            {
                car = car.Where(s => s.CarBrand.BrendName.Contains(searchString));
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            int pageSize1 = 3;
            int pageNumber1 = (page1 ?? 1);

            AdminViewModels aModel = new AdminViewModels() { CarBrands = carBrands.ToPagedList(pageNumber, pageSize),
            Cars=car.ToPagedList(pageNumber1, pageSize1)
            };

            return View(aModel);
        }
    }
}