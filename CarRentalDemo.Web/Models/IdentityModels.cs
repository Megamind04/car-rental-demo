using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRentalDemo.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please Enter your Name.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter your LastName.")]
        [DisplayName("Surname")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Your must provide a Phone Number.")]
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Driving License")]
        public string DrivingLicenseNumber { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}