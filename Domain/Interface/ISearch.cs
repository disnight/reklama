using Domain.Entity.Shared;
using Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ISearch<T> where T: SearchProviderEntity
    {
        IRepository<T> Repository { get; set; }
        IQueryable<T> Search(string keyword, bool onlyByName);
    }
}
