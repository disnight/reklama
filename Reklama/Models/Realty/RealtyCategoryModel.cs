using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using Domain.Repository.Realty;
using Reklama.Models.Shared;

namespace Reklama.Models.Realty
{
    public class RealtyCategoryModel: BaseModel<RealtyCategory>, IRealtyCategoryRepository
    {
    }
}