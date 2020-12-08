using DataAccessLayer.Context;
using DataAccessLayer.Errors;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
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
        private IQueryable<T> entities;

        public BaseRepository(DatabaseContext Context)
        {
            context = Context;
            entities = context.Set<T>().AsQueryable();
        }

        public IRepository<T> Include(string navigationPropertyName)
        {
            entities = entities.Include(navigationPropertyName);
            return this;
        }

        public void Delete(int id)
        {
            var entity = entities.SingleOrDefault(e => e.Id == id);
            CheckIsEntityNull(entity, id);
            entity.Deleted = true;
            context.SaveChanges();

        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(int id)
        {
            var entity = entities.SingleOrDefault(e => e.Id == id);
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
                throw new EntityNotFoundException($"Entity with specified id: {id} not found");
            }
        }
    }
}
