using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceMock.Models;
using GrpcServiceMock.Protos;
using GrpcServiceMock.Repositories;
using static GrpcServiceMock.Protos.ProductDetailService;

namespace GrpcServiceMock.Services
{
    public class ProductDetailService : ProductDetailServiceBase
    {
        private readonly IRepository<ProductDetail> _repositoryProductDetail;
        public ProductDetailService(IRepository<ProductDetail> repositoryProductDetail)
        {
            _repositoryProductDetail = repositoryProductDetail;
        }
        public override Task<ProductDetailResponse> Insert(ProductDetailProto request, ServerCallContext context)
        {

            try
            {
                ProductDetail productDetail = new ProductDetail();
                productDetail.Color = request.Color;
                productDetail.Price = request.Price;
                productDetail.StartingDate = request.StartingDate.ToDateTime();
                productDetail.UpdatedDate = DateTime.Now;
                productDetail.CreatedDate = DateTime.Now;
                productDetail.ProductId = request.ProductId;
                productDetail.ClosingDate = request.ClosingDate.ToDateTime();
                _repositoryProductDetail.Insert(productDetail);
                return Task.FromResult(new ProductDetailResponse() { Data = request, Status = "Thêm Thành Công" });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ProductDetailResponse() { Data = null, Status = "Thất Bại" });
            }

        }
        public override Task<PagingProductDetailResponse> GetPaging(PagingProductDetailRequest request, ServerCallContext context)
        {
            var response = new PagingProductDetailResponse();
            var count = _repositoryProductDetail.GetAll().Count();
            var pagingProduct = _repositoryProductDetail.GetAll().Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new ProductDetailProto()
                {
                    Id = x.Id,
                    Color = x.Color,
                    Price = x.Price,
                    StartingDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)x.StartingDate, DateTimeKind.Utc)),
                    CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.CreatedDate, DateTimeKind.Utc)),
                    UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.UpdatedDate, DateTimeKind.Utc)),
                    ProductId = x.ProductId,
                    ClosingDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)x.ClosingDate, DateTimeKind.Utc))

                });

            response.Data.AddRange(pagingProduct.ToArray());
            response.Count = count;
            response.PageIndex = request.PageIndex;
            response.PageSize = request.PageSize;
            return Task.FromResult(response);

        }
        public override Task<ProductDetails> GetAll(EmptyProductDetail request, ServerCallContext context)
        {
            ProductDetails ProductDetailsResponse = new ProductDetails();
            var productDetails = _repositoryProductDetail.GetAll().Select(x =>
                new ProductDetailProto()
                {
                    Id=x.Id,
                    Color = x.Color,
                    Price = x.Price,
                    StartingDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)x.StartingDate, DateTimeKind.Utc)),
                    CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.CreatedDate, DateTimeKind.Utc)),
                    UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.UpdatedDate, DateTimeKind.Utc)),
                    ProductId = x.ProductId,
                    ClosingDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)x.ClosingDate, DateTimeKind.Utc))

                }).ToList();
               ProductDetailsResponse.Items.AddRange(productDetails.ToArray());
            return Task.FromResult(ProductDetailsResponse);

        }
        public override Task<ProductDetailResponse> Delete(ProductDetailRowIdFilter request, ServerCallContext context)
        {
            try
            {
                ProductDetails ProductDetailsResponse = new ProductDetails();
                var getById = _repositoryProductDetail.GetById(request.Id);
                if (getById == null)
                {
                    return Task.FromResult<ProductDetailResponse>(new ProductDetailResponse()
                    {
                        Data = null,
                        Status = $"Không có{getById}"
                    });
                }
                _repositoryProductDetail.Delete(getById);
                return Task.FromResult<ProductDetailResponse>(new ProductDetailResponse()
                {
                    Data = MapDataToProto(getById),
                    Status = $"Xóa thành công"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult<ProductDetailResponse>(new ProductDetailResponse()
                {
                    Data = null,
                    Status = $"Xóa fail {ex.Message}"
                });
            }


        }

        public override Task<ProductDetailProto> GetById(ProductDetailRowIdFilter request, ServerCallContext context)
        {

            var productDetail = _repositoryProductDetail.GetById(request.Id);
            if (productDetail == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"not find {request.Id}"));
            }
            var productDetailProto = new ProductDetailProto()
            {
                Id = request.Id,
                Color = productDetail.Color,
                Price = productDetail.Price,
                StartingDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)productDetail.StartingDate, DateTimeKind.Utc)),
                CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(productDetail.CreatedDate, DateTimeKind.Utc)),
                UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(productDetail.UpdatedDate, DateTimeKind.Utc)),
                ProductId = productDetail.ProductId,
                ClosingDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)productDetail.ClosingDate, DateTimeKind.Utc))

            };

            return Task.FromResult(productDetailProto);
        }

        public override Task<ProductDetailResponse> Put(ProductDetailProto request, ServerCallContext context)
        {
            try
            {
                ProductDetails productDetailsResponse = new ProductDetails();
                var getById = _repositoryProductDetail.GetById(request.Id);
                if (getById == null)
                {
                    return Task.FromResult<ProductDetailResponse>(new ProductDetailResponse()
                    {
                        Data = null,
                        Status = $"Không có{request.Id}"
                    });
                }
                getById.Id = request.Id;
                getById.Color = request.Color;
                getById.CreatedDate = DateTime.Now;
                getById.Price = request.Price;
                getById.ProductId= request.ProductId;
                getById.UpdatedDate = DateTime.Now;
                getById.StartingDate = request.StartingDate.ToDateTime();
                getById.ClosingDate = request.ClosingDate.ToDateTime();
                _repositoryProductDetail.Update(getById);
                return Task.FromResult<ProductDetailResponse>(new ProductDetailResponse()
                {
                    Data = request,
                    Status = "Sửa Thành Công"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult<ProductDetailResponse>(new ProductDetailResponse()
                {
                    Data = null,
                    Status = "fail"
                });
            }

        }
        private ProductDetailProto MapDataToProto(ProductDetail request)
        {
            ProductDetailProto productProto = new ProductDetailProto();
            productProto.Id = request.Id;
            productProto.Color = request.Color;
            productProto.CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(request.CreatedDate, DateTimeKind.Utc));
            productProto.Price = request.Price;
            productProto.ClosingDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)request.ClosingDate, DateTimeKind.Utc));
            productProto.ClosingDate = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)request.StartingDate, DateTimeKind.Utc));
            productProto.UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(request.UpdatedDate, DateTimeKind.Utc));
            productProto.ProductId = request.ProductId;

            return productProto;
        }
    }
}
