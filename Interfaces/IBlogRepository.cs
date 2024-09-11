using PetsShop_API_DotNet.Models;

namespace petshop.Interfaces
{
  public interface IBlogRepository
  {
    Task<Blog> CreateBlog(string title, string description, string DOM, string author);
  }
}