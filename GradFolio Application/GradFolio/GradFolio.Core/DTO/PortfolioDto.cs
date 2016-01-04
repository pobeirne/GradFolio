using System;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.DTO
{
    public class PortfolioDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string Type { get; set; }

        public long RefNum { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
