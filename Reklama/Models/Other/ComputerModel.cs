using System.Linq;
using Domain.Entity.Common;
using Domain.Repository.Other;
using Reklama.Models.Shared;

namespace Reklama.Models.Other
{
    public class ComputerModel : BaseModel<Computer>, IComputerRepository
    {
        public Computer GetByComputerKey(string key)
        {
            return Context.Set<Computer>().FirstOrDefault(q => q.Key.Equals(key));
        }
    }
}