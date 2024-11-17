namespace APIStore.Services;

using System.Data;
using APIStore.Repositories.Abstracts;
using APIStore.Models;

public class CategoryService
{
    public ICategoryRepository categoryRepository { get; set; }
    public CategoryService(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    public List<Category> GetCategories()
    {
        return categoryRepository.GetCategories();
    }

    public Category? GetCategory(string name)
    {
        Category? category = categoryRepository.GetCategory(name);
        if (category == null)
        {
            throw new ArgumentException("The category is not found.");
        }
        return category;
    }

    public Category? CreateCategory(string name)
    {
        Category? existingCategory = categoryRepository.GetCategory(name);

        if (existingCategory != null)
        {
            throw new DuplicateNameException();
        }

        Category? newCategory = categoryRepository.CreateCategory(name);

        if (newCategory == null)
        {
            throw new InvalidOperationException("Failed to create the category.");
        }

        return newCategory;
    }


    public Category? DeleteCategory(Guid id)
    {
        Category? category = categoryRepository.DeleteCategory(id);
        if (category == null)
        {
            return null;
        }
        return category;
    }
}