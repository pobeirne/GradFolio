using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.ViewModels
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Course Title:")]
        [Required(ErrorMessage = "Please provide a course Title.")]
        public string Title { get; set; }

        [Display(Name = "College Name:")]
        [Required(ErrorMessage = "Please provide a course College.")]
        public string College { get; set; }

        [Display(Name = "Course Summary:")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please provide a course Summary.")]
        public string Summary { get; set; }

        [Display(Name = "Location:")]
        [Required(ErrorMessage = "Please provide a course Institution.")]
        public string Location { get; set; }

        [Display(Name = "Start Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please provide a course Start Date.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }


        [Display(Name = "Currently Attending:")]
        public bool IsCurrent { get; set; }
    }
}