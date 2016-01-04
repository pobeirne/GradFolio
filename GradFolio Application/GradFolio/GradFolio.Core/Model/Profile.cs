using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradFolio.Core.Model
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
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

        [Column("Mobile ")]
        [StringLength(16)]
        public string Mobile { get; set; }

        [Column("Phone ")]
        [StringLength(16)]
        public string Phone { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime? AvailableFromDate { get; set; }

        public string ImageUrl { get; set; }

        public string PortfolioUrl { get; set; }

        public string LinkedInUrl { get; set; }

         [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
