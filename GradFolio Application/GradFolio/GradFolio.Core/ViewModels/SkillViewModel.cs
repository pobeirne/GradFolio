using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.ViewModels
{
    public class SkillViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Please provide a skill Title.")]
        [StringLength(150)]
        public string Title { get; set; }
        
        [Display(Name = "Level:")]
        [Required(ErrorMessage = "Please provide a skill Level.")]
        [StringLength(150)]
        public string Level { get; set; }

        [Display(Name = "Summary:")]
        [Required(ErrorMessage = "Please provide a skill Description.")]
        [StringLength(600)]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Display(Name = "Create Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
    }
}