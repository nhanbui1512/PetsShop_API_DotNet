using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace petshop.Validations
{
    public class MinimumItems : ValidationAttribute
    {
        private readonly int _minItems;
        public MinimumItems(int minItems)
        {
            _minItems = minItems;
            ErrorMessage = $"The field must contain at least {_minItems} item(s).";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as ICollection;
            if (list != null && list.Count >= _minItems)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}