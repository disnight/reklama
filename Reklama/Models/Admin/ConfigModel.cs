using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Repository.Admin;
using Domain.Entity.Admin;
using Reklama.Models.Shared;

namespace Reklama.Models.Admin
{
    public class ConfigModel: BaseModel<Config>, IConfigRepository
    {
        public Config ReadByName(string name)
        {
            try
            {
                return Context.Set<Config>().FirstOrDefault(c => c.Name == name);
            }
            catch
            {
                return null;
            }
        }
    }
}