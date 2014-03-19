using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Common;
using Domain.Repository.Shared;

namespace Domain.Repository.Other
{
    public interface IComputerRepository : IRepository<Computer>
    {
        Computer GetByComputerKey(string key);
    }
}
