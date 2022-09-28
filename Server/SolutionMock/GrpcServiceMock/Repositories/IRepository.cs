using GrpcServiceMock.Models;
using GrpcServiceMock.Protos;

namespace GrpcServiceMock.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Delete(T entity);
    }
}
