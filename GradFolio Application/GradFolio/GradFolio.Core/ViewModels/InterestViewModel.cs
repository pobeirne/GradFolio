using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.ViewModels
{
    public class InterestViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Please provide a Interest Title.")]
        [StringLength(150)]
        public string Title { get; set; }
        
        [Display(Name = "Summary:")]
        [StringLength(600)]
        public string Summary { get; set; }

        [Display(Name = "Create Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
    }
}