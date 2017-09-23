using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDemo.Entities
{
    public class Car
    {
        public int CarID { get; set; }
        public int CarBrendID { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please Enter Description.")]
        [StringLength(50, ErrorMessage = "Car Description to Long.")]
        public string CarDescription { get; set; }

        [DisplayName("Car Image")]
        [Required(ErrorMessage = "Please Enter Link Image.")]
        [RegularExpression(@".*\.png", ErrorMessage = "Not Allowed Image Format.")]
        public string CarImage { get; set; }

        [Required(ErrorMessage = "Please Enter Registration Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Registration Date")]
        public DateTime? CarRegistrationDate { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please Enter Price Per Day.")]
        [Display(Name = "Price Per Day")]
        public decimal? PricePerDay { get; set; }

        [DefaultValue(true)]
        public bool Available { get; set; }


        public virtual CarBrand CarBrand { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
