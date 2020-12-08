using DataAccessLayer.Context;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext context;

        public BaseRepository(DatabaseContext Context)
        {
            context = Context;
        }

        public void Delete(int id)
        {
            var entity = context.Set<T>().SingleOrDefault(e => e.Id == id);
            CheckIsEntityNull(entity, id);
            entity.Deleted = true;
            context.SaveChanges();

        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsEnumerable();
        }

        public T GetById(int id)
        {
            var entity = context.Set<T>().SingleOrDefault(e => e.Id == id);
            CheckIsEntityNull(entity, id);
            return entity;
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        private void CheckIsEntityNull(T entity, int id)
        {
            if (entity == null)
            {
                throw new Exception($"Entity with specified id: {id} not found");
            }
        }
    }
}
