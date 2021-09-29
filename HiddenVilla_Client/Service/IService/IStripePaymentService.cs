using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> Checkout(StripePaymentDTO model);
    }
}