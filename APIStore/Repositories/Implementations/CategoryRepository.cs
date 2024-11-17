using APIStore.Data;
using APIStore.Models;
using APIStore.Repositories.Abstracts;

namespace APIStore.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
  public DataContext Context { get; set; }

  public CategoryRepository(DataContext context)
  {
    Context = context;
  }

  public List<Category> GetCategories()
  {
    return Context.Categories.ToList();
  }

  public Category? GetCategory(string name)
  {
    Category? category = Context.Categories.Where(c => c.Name == name).FirstOrDefault();
    return category;
  }

  public Category? CreateCategory(string name)
  {
    Category? category = Context.Categories.Where(c => c.Name == name).FirstOrDefault();
    if (category != null)
    {
      return null;
    }

    Category newCategory = new(name);
    Context.Categories.Add(newCategory);
    Context.SaveChanges();
    return newCategory;
  }

  public Category? DeleteCategory(Guid id)
  {
    Category? category = Context.Categories.Find(id);
    if (category == null)
    {
      return null;
    }

    Context.Categories.Remove(category);
    Context.SaveChanges();
    return category;
  }
}