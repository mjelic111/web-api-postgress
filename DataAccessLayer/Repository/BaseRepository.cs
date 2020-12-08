﻿using DataAccessLayer.Context;
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
        private IQueryable<T> entitiesWithInclude;

        public BaseRepository(DatabaseContext Context)
        {
            context = Context;
            entitiesWithInclude = context.Set<T>().AsQueryable();
        }

        public IRepository<T> Include(string navigationPropertyName)
        {
            entitiesWithInclude = context.Set<T>().Include(navigationPropertyName);
            return this;
        }

        public async Task DeleteAsync(int id, bool unitOfWork = false)
        {
            var entity = context.Set<T>().AsNoTracking().SingleOrDefault(e => e.Id == id);
            CheckIsEntityFound(entity, id);
            entity.Deleted = true;
            if (!unitOfWork)
            {
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entitiesWithInclude.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await entitiesWithInclude.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
            CheckIsEntityFound(entity, id);
            return entity;
        }

        public async Task<int> InsertAsync(T entity, bool unitOfWork = false)
        {
            CheckIsEntityNull(entity);
            context.Set<T>().Add(entity);
            if (!unitOfWork)
            {
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task UpdateAsync(T entity, bool unitOfWork = false)
        {
            CheckIsEntityNull(entity);
            //var dbEntity = context.Set<T>().AsNoTracking().SingleOrDefault(e => e.Id == entity.Id);
            //CheckIsEntityFound(entity, entity.Id);
            //// dont override base entites
            //entity.CreatedAt = dbEntity.CreatedAt;
            //entity.Deleted = dbEntity.Deleted;
            //update
            context.Entry(entity).State = EntityState.Modified;
            if (!unitOfWork)
            {
                await context.SaveChangesAsync();
            }
        }

            public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        private void CheckIsEntityNull(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity cannot be null");
            }
        }

        private void CheckIsEntityFound(T entity, int id)
        {
            if (entity == null)
            {
                throw new EntityNotFoundException($"Entity with specified id: {id} not found");
            }
        }

    }
}
