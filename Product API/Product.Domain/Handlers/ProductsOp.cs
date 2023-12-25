using Product.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.Domain.Handlers
{
    public class ProductsOp : IProductsOp
    {
        #region Variables  
        private readonly IRepository<Model.Product> _productModelRepository;
        #endregion
        public ProductsOp(IRepository<Model.Product> productModelRepository)
        {
            _productModelRepository = productModelRepository;
        }
        public List<Model.Product> GetProducts()
        {
            return _productModelRepository.GetAll().Select(_ => new Product.Domain.Model.Product() { Id = _.Id, Name = _.Name, Quantity = _.Quantity }).Distinct().OrderBy(s => s.Name).ToList();
        }

        public Model.Product GetProductById(int Id)
        {
            if (Id < 1) throw new ArgumentException("product Id can't less than 1");

            var selectProduct = _productModelRepository.GetAll().Where(_ => _.Id == Id).Select(_ => new Product.Domain.Model.Product() { Id = _.Id, Name = _.Name, Quantity = _.Quantity }).FirstOrDefault();
            if (selectProduct == null) throw new ArgumentNullException(nameof(selectProduct));
            return selectProduct;
        }

        public Model.Product AddProduct(Model.Product productToBeSaved)
        {
            if (productToBeSaved == null) throw new ArgumentNullException(nameof(productToBeSaved));
            if (string.IsNullOrEmpty(productToBeSaved.Name)) throw new ArgumentNullException("Product Name is mandatory");
           
            _productModelRepository.Add(productToBeSaved);
            _productModelRepository.UnitOfWork.Commit();
            return GetProductById(productToBeSaved.Id);
        }

        public Model.Product UpdateProduct(int ProductId, Model.Product productToBeSaved)
        {
            if (productToBeSaved == null) throw new ArgumentNullException(nameof(productToBeSaved));
           
            var savedProduct = _productModelRepository.GetAll().Where(_ => _.Id == ProductId).FirstOrDefault();
            if (savedProduct == null)
            {
                throw new ArgumentException("Product is not found");
            }
           
            _productModelRepository.Update(productToBeSaved);
            _productModelRepository.UnitOfWork.Commit();
            return GetProductById(savedProduct.Id);
        }

        bool IProductsOp.DeleteProduct(int ProductId)
        {
            var savedProduct = _productModelRepository.GetAll().Where(_ => _.Id == ProductId).FirstOrDefault();
            if (savedProduct == null)
            {
                throw new ArgumentException("Product is not found");
            }

            _productModelRepository.Delete(savedProduct);
            _productModelRepository.UnitOfWork.Commit();
            return true;
        }
    }
}
