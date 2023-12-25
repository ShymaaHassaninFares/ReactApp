using System.Collections.Generic;

namespace Product.Domain.Interface
{
    public interface IProductsOp
    {
        List<Model.Product> GetProducts();
        Model.Product GetProductById(int Id);
        Model.Product AddProduct(Model.Product product);
        Model.Product UpdateProduct(int ProductId, Model.Product product);
        bool DeleteProduct(int ProductId);
    }
}
