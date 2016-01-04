using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.ViewModels
{
    public class AwardViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Award Title:")]
        [StringLength(150)]
        [Required(ErrorMessage = "Please provide a award Title.")]
        public string Title { get; set; }

        [Display(Name = "Award Level:")]
        [StringLength(150)]
        [Required(ErrorMessage = "Please provide a award Level.")]
        public string Level { get; set; }

        [Display(Name = "Issued By:")]
        [StringLength(150)]
        [Required(ErrorMessage = "Please provide a awarding body.")]
        public string IssuedBy { get; set; }

        [Display(Name = "Issued Date:")]
        [Required(ErrorMessage = "Please provide a award Issue Date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssuedDate { get; set; }
    }
}