using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entity.Shared
{
    public class Currency: BaseEntity
    {
        public string Name { get; set; }
        public float Rate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}