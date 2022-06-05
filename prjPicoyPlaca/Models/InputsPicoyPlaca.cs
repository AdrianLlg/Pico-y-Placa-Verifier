using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjPicoyPlaca.Models
{
    public class InputsPicoyPlaca
    {
        [Display(Name = "Plate Number:", AutoGenerateFilter = false)]
        [Required(ErrorMessage = "Please, fill the information required.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "Please, provide a valid Plate Number.")]
        [RegularExpression(@"^[0-9]{0,9}$", ErrorMessage = "The entry must be numeric characters only.")]
        public string plateNumber { get; set; }

        [Display(Name = "Date:", AutoGenerateFilter = false)]
        [Required(ErrorMessage = "Please, fill out the information required.")]
        public string date { get; set; }

        [Display(Name = "Time:", AutoGenerateFilter = false)]
        [Required(ErrorMessage = "Please, fill the information required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        public TimeSpan time { get; set; }
    }
}