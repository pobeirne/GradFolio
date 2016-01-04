using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradFolio.Core.Model
{
    public class CurriculumVitae
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

         [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long RefNum { get; set; }

        [Required]
        [StringLength(128)]
        public string Experience1 { get; set; }

        [Required]
        [StringLength(128)]
        public string Experience2 { get; set; }

        [Required]
        [StringLength(128)]
        public string Course1 { get; set; }

        [Required]
        [StringLength(128)]
        public string Course2 { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
