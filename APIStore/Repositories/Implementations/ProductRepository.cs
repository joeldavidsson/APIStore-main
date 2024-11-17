using APIStore.Data;
using APIStore.Models;
using APIStore.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace APIStore.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    public DataContext Context { get; set; }

    public ProductRepository(DataContext context)
    {
        Context = context;
    }

    public List<Product> GetProducts()
    {
        return Context.Products.Include(p => p.Category).ToList();
    }

    public Product? GetProduct(string name)
    {
        Product? product = Context.Products.Where(p => p.Name == name).Include(p => p.Category).FirstOrDefault();
        return product;
    }

    public List<Product> GetProductsWithCategory(string name)
    {
        List<Product> products = Context.Products.Where(p => p.Category!.Name == name).Include(p => p.Category).ToList();
        return products;
    }

    public List<Product> GetProductsInRange(double minPrice, double maxPrice)
    {
        List<Product> products = Context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).Include(p => p.Category).ToList();
        return products;
    }

    public Product? CreateProduct(string name, double price, int amount, string categoryName)
    {
        Product? product = Context.Products.Where(p => p.Name == name).FirstOrDefault();
        if (product != null)
        {
            return null;
        }

        Category? category = Context.Categories.Where(c => c.Name == categoryName).FirstOrDefault();
        if (category == null)
        {
            return null;
        }

        Product newProduct = new(name, price, amount, category);
        Context.Products.Add(newProduct);
        Context.SaveChanges();
        return newProduct;
    }

    public Product? ChangeAmount(Guid id, int newAmount)
    {
        Product? product = Context.Products.Find(id);
        if (product == null)
        {
            return null;
        }

        product.Amount = newAmount;
        Context.SaveChanges();
        return product;
    }

    public Product? DeleteProduct(Guid id)
    {
        Product? product = Context.Products.Find(id);
        if (product == null)
        {
            return null;
        }

        Context.Products.Remove(product);
        Context.SaveChanges();
        return product;
    }
}