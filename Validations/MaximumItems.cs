using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace petshop.Validations
{
    public class MaximumItems : ValidationAttribute
    {
        private readonly int _maxItems;
        public MaximumItems(int minItems)
        {
            _maxItems = minItems;
            ErrorMessage = $"The field must contain at least {_maxItems} item(s).";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as ICollection;
            if (list != null && list.Count <= _maxItems)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}