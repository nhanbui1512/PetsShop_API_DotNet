using Microsoft.EntityFrameworkCore;
using petshop.Data;
using petshop.Interfaces;
using PetsShop_API_DotNet.Models;

namespace petshop.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;
        public BlogRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<Blog> CreateBlog(string title, string description, string DOM, string author)
        {
            Blog blog = new Blog
            {

                Title = title,
                Description = description,
                DOM = DOM,
                Author = author
            };
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<List<Blog>> GetBlogs()
        {
            var blog = _context.Blogs.AsQueryable();
            var result = await blog.ToListAsync();
            return result;
        }
    }
}