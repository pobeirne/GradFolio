using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "FirstName :")]
        [Required(ErrorMessage = "Please provide a First Name.")]
        public string FirstName { get; set; }

        [Display(Name = "LastName:")]
        [Required(ErrorMessage = "Please provide a Last Name.")]
        public string LastName { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Please provide a Industry Title.")]
        public string Title { get; set; }

        [Display(Name = "Image URL:")]
        [Required(ErrorMessage = "Please provide a Image Link.")]
        public string ImageUrl { get; set; }

        [Display(Name = "Summary:")]
        [Required(ErrorMessage = "Please provide a Summary.")]
        [StringLength(600)]
        [DataType(DataType.MultilineText)] 
        public string Summary { get; set; }

        [Display(Name = "Location:")]
        [Required(ErrorMessage = "Please provide a Location.")]
        public string Location { get; set; }
        
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Phone:")]
        public string Phone { get; set; }

        [Display(Name = "Mobile:")]
        public string Mobile { get; set; }

        [Display(Name = "LinkedIn:")]
        public string LinkedInUrl { get; set; }
        
        [Display(Name = "Portfolio:")]
        public string PortfolioUrl { get; set; }
        
        [Display(Name = "Join Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }

        [Display(Name = "Available From:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AvailableFromDate { get; set; }

        [Display(Name = "Available:")]
        public bool IsAvailable { get; set; }
    }
}
