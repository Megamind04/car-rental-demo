using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarRentalDemo.Entities;

namespace CarRentalDemo.Web.Models
{
    public class SelectedCarsPerBrand
    {
        public CarBrand SelectedCarBrand { get; set; }
        public IEnumerable<Car> SelectedCars { get; set; }
    }
}