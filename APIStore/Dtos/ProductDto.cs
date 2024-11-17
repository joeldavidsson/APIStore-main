namespace APIStore.Dtos;

using APIStore.Models;

public class ProductDto
{
  public string Name { get; set; } = "";
  public double Price { get; set; } = 0;
  public int Amount { get; set; } = 0;
  public CategoryDto? Category { get; set; }

  public ProductDto() {}

  public ProductDto(Product product)
  {
    this.Name = product.Name;
    this.Price = product.Price;
    this.Amount = product.Amount;
    this.Category = new CategoryDto(product.Category!.Name);
  }
}