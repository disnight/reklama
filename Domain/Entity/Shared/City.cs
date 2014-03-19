using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entity.Shared
{
    public class City: BaseEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}