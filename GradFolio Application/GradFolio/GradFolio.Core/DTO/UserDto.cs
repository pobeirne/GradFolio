using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.DTO
{
    public class UserDto
    {
        public string Id { get; set; }

        [StringLength(256)]
        public string Email { get; set; }
    }
}
