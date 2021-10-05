using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace HiddenVilla_Client.Service.IService
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> Checkout(StripePaymentDTO model);
    }
}