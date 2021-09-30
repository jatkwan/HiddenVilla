using Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomOrderController : Controller
    {
        private readonly IRoomOrderDetailRepository _repository;

        public RoomOrderController(IRoomOrderDetailRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomOrderDetailDTO roomOrderDetail)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Create(roomOrderDetail);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating Room Details / Booking"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PaymentSucessful([FromBody] RoomOrderDetailDTO roomOrderDetail)
        {
            var service = new SessionService();
            var sessionDetail = service.Get(roomOrderDetail.StripeSessionId);
            if (sessionDetail.PaymentStatus == "paid")
            {
                var result = await _repository.MarkPaymentSuccessful(roomOrderDetail.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        ErrorMessage = "Can not mark payment sucessful"
                    });
                }
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Can not mark payment sucessful"
                });
            }
        }
    }
}