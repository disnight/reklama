using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Repository.Realty;
using Domain.Entity.Realty;
using Reklama.Models.Shared;

namespace Reklama.Models.Realty
{
    public class RealtySectionModel: BaseModel<RealtySection>, IRealtySectionRepository
    {
    }
}