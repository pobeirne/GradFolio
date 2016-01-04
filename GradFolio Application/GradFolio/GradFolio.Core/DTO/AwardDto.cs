using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.DTO
{
    public class AwardDto
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
        public string Level { get; set; }

        [StringLength(150)]
        public string IssuedBy { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
