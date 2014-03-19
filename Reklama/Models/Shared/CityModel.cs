using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using Domain.Repository.Shared;

namespace Reklama.Models.Shared
{
    public class CityModel: BaseModel<City>, ICityRepository
    {
    }
}