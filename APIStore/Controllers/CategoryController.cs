namespace APIStore.Models;

using APIStore.Services;
using Microsoft.AspNetCore.Mvc;
using APIStore.Dtos;
using System.Data;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    private CategoryService categoryService;
    public CategoryController(CategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpPost("add")]
    [Authorize]
    public IActionResult CreateCategory([FromBody] CategoryDto dto)
    {
        try
        {
            Category? category = categoryService.CreateCategory(dto.Name);
            return Ok(category);
        }
        catch (DuplicateNameException)
        {
            return Conflict($"Category with the name '{dto.Name}' already exists.");
        }
    }

    [HttpGet("get")]
    [Authorize]
    public IActionResult GetCategory([FromQuery] CategoryDto dto)
    {
        Category? category = categoryService.GetCategory(dto.Name);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpGet("all")]
    [Authorize]
    public List<Category> GetCategories()
    {
        return categoryService.GetCategories();
    }

    [HttpDelete("delete/{id}")]
    [Authorize]
    public IActionResult DeleteCategory(Guid id)
    {
        try
        {
            Category? category = categoryService.DeleteCategory(id);
            return Ok(category);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}
