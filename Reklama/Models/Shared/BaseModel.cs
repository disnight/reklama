using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using Domain.Repository.Shared;
using System.Data;
using Reklama.Models;
using System.Data.Entity;

namespace Reklama.Models.Shared
{
    public class BaseModel<T> : IRepository<T>
        where T : BaseEntity
    {
        private ReklamaContext _context = null;

        public DbContext Context
        {
            get
            {
                if (_context != null)
                {
                    return _context;
                }
                else
                {
                    throw new DataException("The DbContext cannot be null");
                }
            }
            set
            {
                if (value != null)
                {
                    _context = (ReklamaContext)value;
                }
            }
        }

        public virtual int Save(T entity)
        {
            T entitydb;

            if (entity.Id != 0)
            {
                entitydb = entity;
                Context.Set<T>().AddOrUpdate(entity);
            }
            else
            {
                entitydb = Context.Set<T>().Add(entity);
            }

            Context.SaveChanges();
            return entitydb.Id;
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public virtual T Read(int id)
        {
            return (from r in Context.Set<T>() where r.Id.Equals(id) select r).FirstOrDefault();
        }

        public virtual IQueryable<T> Read()
        {
            return from r in Context.Set<T>() select r;
        }


        public virtual IQueryable<T> FilterCollection(Func<T, bool> predicate)
        {
            return Context.Set<T>().Where(predicate).AsQueryable();
        }

        public virtual T FilterOne(Func<T, bool> predicate)
        {
            return Context.Set<T>().First(predicate);
        }

        public virtual IQueryable<T> Filter(IQueryable<T> collection, Func<T, bool> predicate)
        {
            return collection.Where(predicate).AsQueryable();
        }
    }
}