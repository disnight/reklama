using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository.Shared;
using Domain.Entity.Admin;

namespace Domain.Repository.Admin
{
    public interface IConfigRepository : IRepository<Config>
    {
        Config ReadByName(string name);
    }
}
