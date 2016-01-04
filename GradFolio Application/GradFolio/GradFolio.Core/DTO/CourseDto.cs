using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.DTO
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        public string College { get; set; }

        [Required]
        [StringLength(600)]
        public string Summary { get; set; }

        [Required]
        [StringLength(150)]
        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
