using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.ViewModels
{
    public class ExperienceViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Position Title:")]
        [Required(ErrorMessage = "Please provide a position Title.")]
        [StringLength(100)]
        public string Title { get; set; }

        [Display(Name = "Company:")]
        [Required(ErrorMessage = "Please provide a position Title.")]
        [StringLength(100)]
        public string Company { get; set; }
        
        [Display(Name = "Summary:")]
        [Required(ErrorMessage = "Please provide a position Title.")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Display(Name = "Location:")]
        [Required(ErrorMessage = "Please provide a position Title.")]
        [StringLength(150)]
        public string Location { get; set; }

        [Display(Name = "Start Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please provide a position Title.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Current Ocupation:")]
        public bool IsCurrent { get; set; }
    }
}