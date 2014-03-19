using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;
using Reklama.Models.Shared;

namespace Reklama.Models.Announcements
{
    public class SectionModel : BaseModel<Section>, ISectionRepository
    {
        public Section ReadByName(string name)
        {
            return Context.Set<Section>().First(s => s.Name.Equals(name));
        }
    }
}