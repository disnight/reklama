using Domain.Entity.Admin;
using Domain.Repository.Admin;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Admin
{
    public class PaymentModel: BaseModel<Payment>, IPaymentRepository
    {
    }
}