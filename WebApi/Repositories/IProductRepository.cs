using WebApi.Entities;

namespace WebApi.Repositories;

public interface IProductRepository
{
    public Product Create(Product product);
    public void UpdateById(int id, Product product);
    public void DeleteById(int id);
    public Product GetById(int id);
    public List<Product> GetAll(string title, string order);
    public bool IsExistsProductById(int id);
}