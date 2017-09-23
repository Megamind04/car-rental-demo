using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarRentalDemo.Entities;
using PagedList;

namespace CarRentalDemo.Web.Models
{
    public class AdminViewModels
    {
        public IEnumerable<CarBrand> CarBrands { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Rent> Rents { get; set; }
    }
}