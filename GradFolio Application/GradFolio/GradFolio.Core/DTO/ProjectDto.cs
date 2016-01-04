using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.DTO
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(600)]
        public string Summary { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
