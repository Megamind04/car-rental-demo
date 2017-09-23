using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDemo.Entities
{
    public class CarRentalUser
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please Enter your Name.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter your LastName.")]
        [DisplayName("Surname")]
        public string SurName { get; set; }

        public string FullName { get { return FirstName + "" + SurName; } }

        [Required(ErrorMessage = "Your must provide a Phone Number.")]
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Driving License")]
        public string DrivingLicenseNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
