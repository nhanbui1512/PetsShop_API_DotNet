using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace petshop.Validations
{
  class ImageValidaton : ValidationAttribute
  {
    private readonly string[] _extensions;
    public ImageValidaton(string[] extensions)
    {
      _extensions = extensions;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var file = value as IFormFile;
      if (file != null)
      {
        var extension = Path.GetExtension(file.FileName);
        if (!_extensions.Contains(extension.ToLower()))
        {
          return new ValidationResult(GetErrorMessage());
        }
      }
      return ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
      return "Only allow upload image file (jpg, jpeg, png).";
    }
  }
}