using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceMock.Models;
using GrpcServiceMock.Protos;
using GrpcServiceMock.Repositories;
using Microsoft.EntityFrameworkCore;
using static GrpcServiceMock.Protos.CatagoryService;
using Empty = GrpcServiceMock.Protos.Empty;

namespace GrpcServiceMock.Services
{
    public class CategoryService: CatagoryServiceBase
    {
        private readonly IRepository<Category> _repository;
        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public override Task<CategoryResponse> Insert(CategoryProto request, ServerCallContext context)
        {
            try
            {
                if(request == null)
                {
                    return Task.FromResult<CategoryResponse>(new CategoryResponse()
                    {
                        Data = request,
                        Status = "Thêm Thành Công"
                    });
                }
                Category categoryMapWithProto = MapDataToEntity(request);
                _repository.Insert(categoryMapWithProto);
                return Task.FromResult<CategoryResponse>(new CategoryResponse()
                {
                    Data = request,
                    Status = "Thêm Thành Công"
                }) ;
            }
            catch (Exception ex)
            {
                return Task.FromResult<CategoryResponse>(new CategoryResponse()
                {
                    Data = null,
                    Status = "fail"
                });
            }
            
        }
        public override Task<PagingCategoryResponse> GetPaging(PagingCategoryRequest request, ServerCallContext context)
        {
            var response = new PagingCategoryResponse();
            var count = _repository.GetAll().Count();
            var pagingUser = _repository.GetAll().Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new CategoryProto()
                {
                    Name = x.Name,
                    Active = (bool)x.Active,
                    CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.CreatedDate, DateTimeKind.Utc)),
                    Id = x.Id,
                    TagName = x.TagName,
                    UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.UpdatedDate, DateTimeKind.Utc))
                });

            response.Data.AddRange(pagingUser.ToArray());
            response.Count = count;
            response.PageIndex = request.PageIndex;
            response.PageSize = request.PageSize;
            return Task.FromResult(response);

        }
        public override Task<Categories> GetAll(Empty request, ServerCallContext context)
        {
            Categories categoriesResponse = new Categories();
            var categories = _repository.GetAll().Select( x =>
                new CategoryProto()
                {
                    Name = x.Name,
                    Active = (bool)x.Active,
                    CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.CreatedDate, DateTimeKind.Utc)),
                    Id = x.Id,
                    TagName = x.TagName,
                    UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(x.UpdatedDate, DateTimeKind.Utc)) 
                }).ToList();
            categoriesResponse.Items.AddRange(categories.ToArray());
            return Task.FromResult(categoriesResponse);

        }
        public override Task<CategoryResponse> Delete(CategoryRowIdFilter request, ServerCallContext context)
        {
            try
            {
                Categories categoriesResponse = new Categories();
                var getById = _repository.GetById(request.Id);
                if (getById == null)
                {
                    return Task.FromResult<CategoryResponse>(new CategoryResponse()
                    {
                        Data = null,
                        Status = $"Không có{getById}"
                    });
                }
                _repository.Delete(getById);
                return Task.FromResult<CategoryResponse>(new CategoryResponse()
                {
                    Data = MapDataToProto(getById),
                    Status = $"Xóa thành công"
                });
            }
            catch(Exception ex)
            {
                return Task.FromResult<CategoryResponse>(new CategoryResponse()
                {
                    Data = null,
                    Status = $"Xóa fail {ex.Message}"
                });
            }
   

        }

        public override Task<CategoryResponse> Put(CategoryProto request, ServerCallContext context)
        {
            try
            {
                Categories categoriesResponse = new Categories();
                var getById = _repository.GetById(request.Id);
                if (getById == null)
                {
                    return Task.FromResult<CategoryResponse>(new CategoryResponse()
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
                _repository.Update(getById);
                return Task.FromResult<CategoryResponse>(new CategoryResponse()
                {
                    Data = request,
                    Status = "Sửa Thành Công"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult<CategoryResponse>(new CategoryResponse()
                {
                    Data = null,
                    Status = "fail"
                });
            }

        }

        private Category MapDataToEntity(CategoryProto request)
        {
            Category category = new Category();
            category.Id = request.Id;   
            category.Name = request.Name;
            category.CreatedDate = DateTime.Now;
            category.Active = request.Active;
            category.TagName = request.TagName;
            category.UpdatedDate = DateTime.Now;
            return category;
        }

        private CategoryProto MapDataToProto(Category request)
        {
            CategoryProto categoryProto = new CategoryProto();
            categoryProto.Id = request.Id;
            categoryProto.Name = request.Name;
            categoryProto.CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
            categoryProto.Active = (bool)request.Active;
            categoryProto.TagName = request.TagName;
            categoryProto.UpdatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
            return categoryProto;
        }

    }
}
