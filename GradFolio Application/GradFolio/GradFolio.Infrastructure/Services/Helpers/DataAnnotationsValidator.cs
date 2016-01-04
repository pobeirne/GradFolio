using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Infrastructure.Services.Helpers
{
    public class DataAnnotationsValidator
    {
        public bool IsValidModel(object @object, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(@object, null, null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                @object, context, results, true
            );
        }
    }
}
