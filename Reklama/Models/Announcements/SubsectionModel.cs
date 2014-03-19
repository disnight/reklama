using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;
using Domain.Repository.Announcements;
using Reklama.Models.Shared;

namespace Reklama.Models.Announcements
{
    public class SubsectionModel : BaseModel<Subsection>, ISubsectionRepository
    {
        public Subsection ReadByName(string name)
        {
            return Context.Set<Subsection>().Include("Section").First(ss => ss.Name.Equals(name));
        }

        public IQueryable<Subsection> ReadBySection(int sectionId)
        {
            return
                from s in Context.Set<Subsection>().Include("Section") 
                where s.SectionId.Equals(sectionId) 
                orderby s.Id
                select s;
        }


        public IEnumerable<Subsection> ReadLimit(int sectionId, int limit)
        {
            return Context.Set<Subsection>().Include("Section").Where(s => s.SectionId == sectionId).Take(limit).ToArray();
        }
    }
}