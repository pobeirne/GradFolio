using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.DTO
{
    public class CurriculumVitaeDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Type { get; set; }

        public long RefNum { get; set; }

        [Required]
        [StringLength(150)]
        public string Experience1 { get; set; }

        [Required]
        [StringLength(150)]
        public string Experience2 { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Course1 { get; set; }

        [Required]
        [StringLength(150)]
        public string Course2 { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
