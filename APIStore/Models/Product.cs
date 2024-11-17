namespace APIStore.Models;

public class Product
{
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public double Price { get; set; }
  public int Amount { get; set; }
  public Category? Category { get; set; }

  public Product()
  {

  }

  public Product(string name, double price, int amount, Category category)
  {
    Name = name;
    Price = price;
    Amount = amount;
    Category = category;
  }
}