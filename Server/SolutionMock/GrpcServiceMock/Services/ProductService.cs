using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceMock.Models;
using GrpcServiceMock.Protos;
using GrpcServiceMock.Repositories;
using Microsoft.EntityFrameworkCore;
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
                product.TagName = "hhh";
                product.Active = request.Active;
                product.UpdatedDate = DateTime.Now;  
                product.CreatedDate = DateTime.Now;
                product.CategoryId = request.CategoryId;
                Product productMapWithProto = MapDataToEntity(request);
                _repositoryProduct.Insert(productMapWithProto);
                return Task.FromResult(new ProductResponse() { Data = request});
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ProductResponse() { Data = null });
            }
                
        }
        public override Task<PagingProductResponse> GetPaging(PagingProductRequest request, ServerCallContext context)
        {
            var response = new PagingProductResponse();
            var count = _repositoryProduct.GetAll().Count();
            var pagingProduct = _repositoryProduct.GetAll().Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new ProductProto()
                {
                    Name = x.Name,
                    Active = (bool)x.Active,
                    CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.CreatedDate, DateTimeKind.Utc)),
                    Id = x.Id,
                    TagName = x.TagName,
                    UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.UpdatedDate, DateTimeKind.Utc)),
                    CategoryId = x.CategoryId
                });

            response.Data.AddRange(pagingProduct.ToArray());
            response.Count = count;
            response.PageIndex = request.PageIndex;
            response.PageSize = request.PageSize;
            return Task.FromResult(response);

        }
        public override Task<Products> GetAll(EmptyProduct request, ServerCallContext context)
        {
            Products ProductsResponse = new Products();
            var products = _repositoryProduct.GetAll().Select(x =>
                new ProductProto()
                {
                    Name = x.Name,
                    Active = (bool)x.Active,
                    CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.CreatedDate, DateTimeKind.Utc)),
                    Id = x.Id,
                    TagName = x.TagName,
                    UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.UpdatedDate, DateTimeKind.Utc)),
                    CategoryId = x.CategoryId
                }).ToList();
            ProductsResponse.Items.AddRange(products.ToArray());
            return Task.FromResult(ProductsResponse);

        }
        public override Task<ProductResponse> Delete(ProductRowIdFilter request, ServerCallContext context)
        {
            try
            {
                Products categoriesResponse = new Products();
                var getById = _repositoryProduct.GetById(request.Id);
                if (getById == null)
                {
                    return Task.FromResult<ProductResponse>(new ProductResponse()
                    {
                        Data = null,
                        Status = $"Không có{getById}"
                    });
                }
                _repositoryProduct.Delete(getById);
                return Task.FromResult<ProductResponse>(new ProductResponse()
                {
                    Data = MapDataToProto(getById),
                    Status = $"Xóa thành công"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult<ProductResponse>(new ProductResponse()
                {
                    Data = null,
                    Status = $"Xóa fail {ex.Message}"
                });
            }


        }

        public override Task<ProductProto> GetById(ProductRowIdFilter request, ServerCallContext context)
        {

            var product = _repositoryProduct.GetById(request.Id);
            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"not find {request.Id}"));
            }
            var productProto = new ProductProto()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(product.CreatedDate, DateTimeKind.Utc)),
                Active = (bool)product.Active,
                TagName = product.TagName,  
                UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(product.UpdatedDate, DateTimeKind.Utc))

            };

            return Task.FromResult(productProto);
        }

        public override Task<ProductResponse> Put(ProductProto request, ServerCallContext context)
        {
            try
            {
                Products categoriesResponse = new Products();
                var getById = _repositoryProduct.GetById(request.Id);
                if (getById == null)
                {
                    return Task.FromResult<ProductResponse>(new ProductResponse()
                    {
                        Data = null,
                        Status = $"Không có{request.Id}"
                    });
                }
                getById.Id = request.Id;
                getById.Name = request.Name;
                getById.CreatedDate = DateTime.Now;
                getById.Active = request.Active;
                getById.TagName = request.TagName;
                getById.UpdatedDate = DateTime.Now;
                getById.CategoryId = request.CategoryId;    
                _repositoryProduct.Update(getById);
                return Task.FromResult<ProductResponse>(new ProductResponse()
                {
                    Data = request,
                    Status = "Sửa Thành Công"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult<ProductResponse>(new ProductResponse()
                {
                    Data = null,
                    Status = "fail"
                });
            }

        }
        private Product MapDataToEntity(ProductProto request)
        {
            Product product = new Product();
            product.Id = request.Id;
            product.Name = request.Name;
            product.CreatedDate =DateTime.Now;
            product.Active = (bool)request.Active;
            product.TagName = request.TagName;
            product.UpdatedDate = DateTime.Now;
            product.CategoryId = request.CategoryId;

            return product;
        }
        private ProductProto MapDataToProto(Product request)
        {
            ProductProto productProto = new ProductProto();
            productProto.Id = request.Id;
            productProto.Name = request.Name;
            productProto.CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
            productProto.Active = (bool)request.Active;
            productProto.TagName = request.TagName;
            productProto.UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
            productProto.CategoryId = request.CategoryId;

            return productProto;
        }
    }
}
