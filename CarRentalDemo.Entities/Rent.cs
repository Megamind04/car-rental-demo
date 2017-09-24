using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDemo.Entities
{
    public class Rent : IValidatableObject
    {
        public int RentID { get; set; }
        public int CarID { get; set; }

        [ForeignKey("CarRentalUser")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Please Select a Date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }

        [Required(ErrorMessage = "Please Select a Date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Rent")]
        //[ValidationDate]
        public DateTime? DateOfRent { get; set; }

        [Required(ErrorMessage = "Please Select a Date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Return")]
        public DateTime? DateOfReturn { get; set; }
            
        [DisplayName("Pay Metod")]
        [Required(ErrorMessage = "Please Select PayMetod.")]
        public string PayMetod { get; set; }

        [DisplayName("Price")]
        public decimal? Price { get; set; }

        [DefaultValue(false)]
        public bool Returned { get; set; }

        public virtual Car Car { get; set; }
        public virtual CarRentalUser CarRentalUser { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfRent > DateOfReturn)
            {
                yield return new ValidationResult("Date of return must be after date of rent.", new[] { "DateOfReturn" });
            }

            if (DateOfRent < CreateDate)
            {
                yield return new ValidationResult("Rent date can not be greater than current date.", new[] { "DateOfRent" });
            }

        }
    }

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    //public class ValidationDateAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        DateTime _ovadate = Convert.ToDateTime(value);
    //        if (_ovadate > DateTime.Now)
    //        {
    //            return ValidationResult.Success;
    //        }
    //        else
    //        {
    //            return new ValidationResult
    //                ("Rent date can not be greater than current date.");
    //        }
    //    }
    //}
}
