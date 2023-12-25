using Microsoft.AspNetCore.Mvc;
using Product.Domain.Interface;
using System.Collections.Generic;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductsOp _productOp;

        public ProductController(IProductsOp productOp)
        {
            _productOp = productOp;
        }

        [HttpGet]
        public ActionResult<List<Product.Domain.Model.Product>> GetProduct()
        {
            return _productOp.GetProducts();
        }

        [HttpGet("{id}")]
        public ActionResult<Product.Domain.Model.Product> GetProduct(int Id)
        {
            return _productOp.GetProductById(Id);
        }

        [HttpPut("{id}")]
        public ActionResult<Product.Domain.Model.Product> PutProduct(int? id, [FromBody] Product.Domain.Model.Product UpdateProduct)
        {
            return _productOp.UpdateProduct((int)id, UpdateProduct);
        }

        [HttpPost]
        public ActionResult<Product.Domain.Model.Product> PostProduct([FromBody] Product.Domain.Model.Product newProduct)
        {
            return _productOp.AddProduct(newProduct);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteProduct(int id)
        {
            return _productOp.DeleteProduct(id);
        }

    }
}
