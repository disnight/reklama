using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Domain.Entity.Catalogs;
using Domain.Entity.Shared;

namespace Domain.Entity.Admin
{
    public class NewSectionInCatalog: BaseEntity
    {
        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual CatalogSecondCategory Category { get; set; }
    }
}