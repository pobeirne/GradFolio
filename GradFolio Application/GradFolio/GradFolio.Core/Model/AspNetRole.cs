using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradFolio.Core.Model
{
    public class AspNetRole
    {
        public AspNetRole()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
