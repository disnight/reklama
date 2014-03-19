using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;
using System.Data.Entity;

namespace Domain.Repository.Shared
{
    public interface IRepository<T> 
        where T: BaseEntity
    {
        DbContext Context { get; set; }
        int Save(T entity);
        void Delete(T entity);
        T Read(int id);
        IQueryable<T> Read();
        IQueryable<T> FilterCollection(Func<T, bool> predicate);
        T FilterOne(Func<T, bool> predicate);
        IQueryable<T> Filter(IQueryable<T> collection, Func<T, bool> predicate);
    }
}
