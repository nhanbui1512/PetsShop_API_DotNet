
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace petshop.Validations
{

    public class PasswordComplexity : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string password)
            {
                // Kiểm tra độ dài tối thiểu
                if (password.Length < 8)
                {
                    return new ValidationResult("Password must be at least 8 characters long.");
                }

                // Kiểm tra có ít nhất một chữ hoa
                if (!Regex.IsMatch(password, @"[A-Z]"))
                {
                    return new ValidationResult("Password must contain at least one uppercase letter.");
                }

                // Kiểm tra có ít nhất một chữ thường
                if (!Regex.IsMatch(password, @"[a-z]"))
                {
                    return new ValidationResult("Password must contain at least one lowercase letter.");
                }

                // Kiểm tra có ít nhất một chữ số
                if (!Regex.IsMatch(password, @"[0-9]"))
                {
                    return new ValidationResult("Password must contain at least one number.");
                }

                // Kiểm tra có ít nhất một ký tự đặc biệt
                if (!Regex.IsMatch(password, @"[\W_]"))
                {
                    return new ValidationResult("Password must contain at least one special character.");
                }
            }

            return ValidationResult.Success;
        }
    }

}