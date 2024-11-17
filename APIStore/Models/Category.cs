namespace APIStore.Models;

public class Category {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public List<Product> Products { get; set; }

  public Category(string name) {
    Name = name;
    Products = new();
  }
}