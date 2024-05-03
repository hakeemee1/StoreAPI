using Microsoft.AspNetCore.Mvc;
using StoreAPI.Data;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase 
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CategoryController(ApplicationDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    //Get: /api/Category
    //get all category in database
    [HttpGet]
    public ActionResult<category> GetCategories()
    {
        var categories = _context.categories
        .Join(
            _context.products,
            c => c.category_id,
            p => p.category_id,
            (c, p) => new
            {
                c.category_id,
                c.category_name,
                p.product_name
            }
        ).ToList();
        return Ok(categories);
    }

    //Get: /api/Category/{id}
    //get category by id
    [HttpGet("{id}")]
    public ActionResult<category> GetCategries(int id) 
    {
        var category = _context.categories.FirstOrDefault(c => c.category_id == id);
        if (category == null) 
        {
            return NotFound();
        } 
        return Ok(category); 
    }

} 