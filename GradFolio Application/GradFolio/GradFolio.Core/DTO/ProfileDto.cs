using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.DTO
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
       
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150)]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(600)]
        public string Summary { get; set; }

        [Required]
        [StringLength(150)]
        public string Location { get; set; }

        [StringLength(16)]
        public string Mobile { get; set; }

        [StringLength(16)]
        public string Phone { get; set; }

        public bool IsAvailable { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AvailableFromDate { get; set; }

        public string ImageUrl { get; set; }

        public string PortfolioUrl { get; set; }

        public string LinkedInUrl { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }
    }
}
