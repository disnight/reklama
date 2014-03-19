using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Announcements;
using Domain.Repository.Shared;

namespace Domain.Repository.Announcements
{
    public interface ISubsectionRepository: IRepository<Subsection>
    {
        Subsection ReadByName(string name);
        IQueryable<Subsection> ReadBySection(int sectionId);
        IEnumerable<Subsection> ReadLimit(int sectionId, int limit);
    }
}
