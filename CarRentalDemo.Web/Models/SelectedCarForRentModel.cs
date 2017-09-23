using CarRentalDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRentalDemo.Web.Models
{
    public class SelectedCarForRentModel
    {
        public Car SelectedCar { get; set; }
        public Rent RentedCar { get; set; }
    }
}