using Grpc.Core;
using GrpcServiceMock.Models;
using GrpcServiceMock.Protos;
using GrpcServiceMock.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Entity;
using static GrpcServiceMock.Protos.ProductService;

namespace GrpcServiceMock.Services
{
    public class ProductService: ProductServiceBase
    {
        
        private readonly IRepository<Product> _repositoryProduct;
        public ProductService(IRepository<Product> repositoryProduct )
        {
            _repositoryProduct = repositoryProduct;
            
        }
        public override Task<ProductResponse> Insert(ProductProto request, ServerCallContext context)
        {
               
            try
            {
                Product product = new Product();
                product.Name = request.Name;
                product.TagName = request.TagName;
                product.Active = request.Active;
                product.UpdatedDate = DateTime.Now;  
                product.CreatedDate = DateTime.Now;
                product.CategoryId = request.CategoryId;  
                _repositoryProduct.Insert(product);
                return Task.FromResult(new ProductResponse() { Data = request});
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ProductResponse() { Data = null });
            }
                
        }
    }
}
