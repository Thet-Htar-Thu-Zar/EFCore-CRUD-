using DotNet7.EFCore_CRUD_.Data;
using DotNet7.EFCore_CRUD_.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet7.EFCore_CRUD_.Controllers;

public class BlogController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public BlogController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #region Get Blog

    [HttpGet]
    [Route("/api/blog")]
    public async Task<IActionResult> GetBlogs()
    {
        try
        {
            var lst = await _appDbContext.Blogs.AsNoTracking().ToListAsync();
            return Ok(lst);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Create Blog

    [HttpPost]
    [Route("/api/blog")]
    public async Task<IActionResult> CreateBlog([FromBody] BlogModel requestModel)
    {
        try
        {
            if (string.IsNullOrEmpty(requestModel.BlogTitle))
                return BadRequest();

            if (string.IsNullOrEmpty(requestModel.BlogAuthor))
                return BadRequest();

            if (string.IsNullOrEmpty(requestModel.BlogContent))
                return BadRequest();

            await _appDbContext.Blogs.AddAsync(requestModel);
            int result = await _appDbContext.SaveChangesAsync();

            return result > 0
                ? StatusCode(201, "Creating Successful!")
                : BadRequest("Creating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Update Blog

    [HttpPut]
    [Route("/api/blog/{id}")]
    public async Task<IActionResult> UpdateBlog([FromBody] BlogRequestModel requestModel, long id)
    {
        try
        {
            if (id <= 0)
                return BadRequest();
            if (string.IsNullOrEmpty(requestModel.BlogTitle))
                return BadRequest();

            if (string.IsNullOrEmpty(requestModel.BlogAuthor))
                return BadRequest();

            if (string.IsNullOrEmpty(requestModel.BlogContent))
                return BadRequest();

            var item = await _appDbContext
                .Blogs.AsNoTracking()
                .FirstOrDefaultAsync(x => x.BlogId == id && x.IsActive);
            if (item is null)
                return NotFound("Blog Not Found or Inactive!");

            item.BlogTitle = requestModel.BlogTitle;
            item.BlogAuthor = requestModel.BlogAuthor;
            item.BlogContent = requestModel.BlogContent;
            _appDbContext.Entry(item).State = EntityState.Modified;

            int result = await _appDbContext.SaveChangesAsync();

            return result > 0
                ? StatusCode(202, "Updating Successful!")
                : BadRequest("Updating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Delete Blog

    [HttpDelete]
    [Route("/api/blog/{id}")]
    public async Task<IActionResult> DeleteBlog(long id)
    {
        try
        {
            if (id <= 0)
                return BadRequest();

            var item = await _appDbContext
                .Blogs.AsNoTracking()
                .FirstOrDefaultAsync(x => x.BlogId == id && x.IsActive);
            if (item is null)
                return NotFound("Blog Not Found or Inactive!");

            item.IsActive = false;
            _appDbContext.Entry(item).State = EntityState.Modified;
            int result = await _appDbContext.SaveChangesAsync();

            return result > 0
                ? StatusCode(202, "Deleting Successful!")
                : BadRequest("Deleting Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion
}
