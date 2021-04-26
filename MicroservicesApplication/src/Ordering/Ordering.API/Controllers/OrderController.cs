using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
        {
            var queryObject = new GetOrderByUserNameQuery(userName);
            var Orders = await _mediator.Send(queryObject);
            return Ok(Orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderResponse>> CheckoutOrder([FromBody]CheckoutOrderCommand orderCommand)
        {
            if (orderCommand == null)
                return BadRequest();
            var Order = await _mediator.Send(orderCommand);
            return Ok(Order);
        }

    }
}
