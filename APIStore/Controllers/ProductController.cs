namespace APIStore.Controllers;

using APIStore.Services;
using APIStore.Models;
using APIStore.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("products")]

public class ProductController : ControllerBase
{
  public ProductService ProductService { get; set; }
  public ProductController(ProductService productService)
  {
    ProductService = productService;
  }

  [HttpPost("add")]
  [Authorize]
  public IActionResult CreateProduct([FromBody] ProductDto dto)
  {
    try
    {
      Product? product = ProductService.CreateProduct(dto.Name, dto.Price, dto.Amount, dto.Category!.Name);
      return Ok(new ProductDto(product!));
    }
    catch (Exception)
    {
      return BadRequest();
    }
  }

  [HttpGet("all")]
  [Authorize]
  public ActionResult<List<ProductDto>> GetProducts()
  {
    try
    {
      List<ProductDto> products = ProductService.GetProducts().Select(product => new ProductDto(product)).ToList();
      return Ok(products);
    }
    catch (Exception)
    {
      return BadRequest();
    }
  }

  [HttpGet("name")]
  [Authorize]
  public IActionResult GetProduct([FromBody] ProductDto dto)
  {
    try
    {
      Product? product = ProductService.GetProduct(dto.Name);
      return Ok(new ProductDto(product!));
    }
    catch (Exception)
    {
      return NotFound();
    }
  }

  [HttpGet("category")]
  [Authorize]
  public ActionResult<List<Product>> GetProductsWithCategory([FromBody] ProductDto dto)
  {
    try
    {
      List<ProductDto> products = ProductService.GetProductsWithCategory(dto.Category!.Name).Select(product => new ProductDto(product)).ToList();
      return Ok(products);
    }
    catch (Exception)
    {
      return NotFound();
    }
  }

  [HttpGet("price")]
  [Authorize]
  public ActionResult<List<ProductDto>> GetProductsInRange([FromQuery] double minPrice, [FromQuery] double maxPrice)
  {
    try
    {
      List<Product> products = ProductService.GetProductsInRange(minPrice, maxPrice);
      List<ProductDto> productDtos = products.Select(product => new ProductDto(product)
   ).ToList();
      return Ok(productDtos);
    }
    catch (Exception)
    {
      return BadRequest();
    }
  }

  [HttpPut("edit")]
  [Authorize]
  public IActionResult ChangeAmount([FromQuery] Guid id, [FromBody] ProductDto dto)
  {
    try
    {
      ProductService.ChangeAmount(id, dto.Amount);
      return Ok();
    }
    catch (Exception)
    {
      return NotFound();
    }
  }

  [HttpDelete("delete")]
  [Authorize]
  public IActionResult DeleteProduct([FromQuery] Guid id)
  {
    try
    {
      ProductService.DeleteProduct(id);
      return Ok();
    }
    catch (Exception)
    {
      return NotFound();
    }
  }
}