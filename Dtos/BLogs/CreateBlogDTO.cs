using System.ComponentModel.DataAnnotations;

namespace petshop.Dtos.Blogs
{

  public class CreateBlogDTO
  {
    [Required]
    [StringLength(300, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters.")]
    public string Title { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Author must be between 5 and 50 characters.")]
    public string Author { get; set; }

    [Required]
    [MinLength(5)]
    public string DOM { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 200 characters.")]
    public string Description { get; set; }

  }
}