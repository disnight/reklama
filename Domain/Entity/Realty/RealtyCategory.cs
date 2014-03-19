using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Entity.Realty
{
    public class RealtyCategory : BaseEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
