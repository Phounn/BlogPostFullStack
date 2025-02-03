using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Share.Models;
using System.Text.Json;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly IRepository repo;
        public BlogPostController(IRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<BlogPost>>> GetAllBlogPost()
        {
            var content = await repo.GetAll();
            return Ok(content);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<BlogPost>>> GetAllBlogPost(string id)
        {
            var content = await repo.GetById(id);
            return Ok(content);
        }
        [HttpPost]
        public async Task<ActionResult<BlogPost>> CreateBlogPost([FromBody] BlogPost blog)
        {
            var content = await repo.Create(blog);
            return Ok(content);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBlogPost([FromBody] BlogPost blog, string id)
        {
            await repo.Update(blog, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogPost(string id)
        {
            await repo.Delete(id);
            return Ok();
        }
        [HttpPost("category/create")]
        public async Task<ActionResult<CategoriesOfBlog>> CreateCategory([FromBody] CategoriesOfBlog cate)
        {
            var content = await repo.CreateCate(cate);
            return Ok();

        }


    }
}
