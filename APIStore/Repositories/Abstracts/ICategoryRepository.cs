namespace APIStore.Repositories.Abstracts;

using APIStore.Models;

public interface ICategoryRepository {
  List<Category> GetCategories();
  Category? GetCategory(string name);
  Category? CreateCategory(string name);
  Category? DeleteCategory(Guid id);
}