using GrpcServiceMock.Models;
using GrpcServiceMock.Protos;
using Microsoft.EntityFrameworkCore;
using System;

namespace GrpcServiceMock.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly NTQTRAININGContext _context;
        public Repository(NTQTRAININGContext context)
        {
            _context = context;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public void Insert(T request)
        {
            _context.Set<T>().Add(request);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
           
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
