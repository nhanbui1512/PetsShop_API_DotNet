using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using petshop.Dtos.Blogs;
using petshop.Interfaces;

namespace petshop.Controllers
{
    [Route("/api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        public BlogsController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newBlog = await _blogRepository.CreateBlog(data.Title, data.Description, data.DOM, data.Author);
            return Ok(newBlog);
        }
    }
}