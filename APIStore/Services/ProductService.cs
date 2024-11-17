namespace APIStore.Services;

using APIStore.Repositories.Abstracts;
using APIStore.Models;

public class ProductService
{ 
    public IProductRepository productRepository { get; set; }
    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public List<Product> GetProducts()
    {   
        return productRepository.GetProducts();
    }

    public Product? GetProduct(string name)
    {
        Product? product = productRepository.GetProduct(name);
        if(product == null)
        {
            throw new ArgumentException("The product is not found.");
        }
        return product;
    }  

    public List<Product> GetProductsInRange(double minPrice, double maxPrice)
    {
        return productRepository.GetProductsInRange(minPrice, maxPrice);
    }


    public List<Product> GetProductsWithCategory(string name)
    {
        return productRepository.GetProductsWithCategory(name);
    }


    public Product? CreateProduct(string name, double price, int amount, string categoryName)
    { 
        Product? product = productRepository.CreateProduct(name, price, amount, categoryName);
        if (product == null)
        {
            throw new ArgumentException("The product already exist.");
        } 
        return product;
    }    


    public Product? ChangeAmount(Guid id, int newAmount)
    {
        Product? product = productRepository.ChangeAmount(id, newAmount);
        if(product == null)
        {
            throw new ArgumentException("No Product with that Id was found");
        } 
        return product;
    }

    public Product? DeleteProduct(Guid id)
    {
        Product? product = productRepository.DeleteProduct(id);
        if(product == null)
        {
            throw new ArgumentException("No Product with that Id was found");
        } 
        return product;
    }
}