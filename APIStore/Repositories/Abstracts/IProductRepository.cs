namespace APIStore.Repositories.Abstracts;

using APIStore.Models;

public interface IProductRepository {
  List<Product> GetProducts();
  Product? GetProduct(string name);
  List<Product> GetProductsWithCategory(string name);
  List<Product> GetProductsInRange(double minPrice, double maxPrice);
  Product? CreateProduct(string name, double price, int amount, string categoryName);
  Product? ChangeAmount(Guid id, int newAmount);
  Product? DeleteProduct(Guid id);
}