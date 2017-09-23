using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDemo.Entities
{
    public class CarBrand
    {
        [Key]
        public int CarBrendID { get; set; }

        [Required(ErrorMessage = "Please Enter Brend Name.")]
        [DisplayName("Brend Name")]
        public string BrendName { get; set; }

        [Required(ErrorMessage = "Please Enter Link Image.")]
        [RegularExpression(@".*\.png", ErrorMessage = "Not Allowed Image Format")]
        [DisplayName("Brend Image")]
        public string BrendImage { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
