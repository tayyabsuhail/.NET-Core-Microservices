using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBus;

        public BasketController(IBasketRepository repository, IMapper mapper, EventBusRabbitMQProducer eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            if (String.IsNullOrEmpty(userName))
                return BadRequest();
            var basketInfo = await _repository.GetBasket(userName);
            return Ok(basketInfo ?? new BasketCart(userName));
        }

        [HttpPut]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart cart)
        {
            if (cart == null)
                return BadRequest();
            var updatedBasketcart = await _repository.UpdateBasket(cart);
            return Ok(updatedBasketcart);
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> DeleteBasket(string userName)
        {
            bool deleteResponse = await _repository.DeleteBasket(userName);
            return Ok(deleteResponse);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody]BasketCheckout basketInfo)
        {
            var basket = await _repository.GetBasket(basketInfo.UserName);
            if (basket == null)
                return BadRequest();
            var popBasket = await _repository.DeleteBasket(basketInfo.UserName);
            if (!popBasket)
                return BadRequest();
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketInfo);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basketInfo.TotalPrice;
            _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            return Accepted();
        }
    }
}
