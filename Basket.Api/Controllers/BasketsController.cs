using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Basket.Application.BasketCardsMongo.Commands.Requests;
using Basket.Application.BasketCardsRedis.Commands.Requests;
using Basket.Application.BasketCardsRedis.Querying.Requests;
using Basket.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [
        ApiController,
        Route("v1/[controller]"),
        Produces(MediaTypeNames.Application.Json)
    ]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public BasketsController(IMediator mediator) =>
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("cache")]
        public async ValueTask<IActionResult> Get([FromQuery] GetBasketItemsQuery request)
        {
            var response = await _mediator.Send(request);
            return this.GetResponse(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("cache")]
        public async ValueTask<IActionResult> Add([FromBody] RedisAddBasketItemCommand request)
        {
            var response = await _mediator.Send(request);
            return this.GetResponse(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> Add([FromBody] AddBasketItemCommand request)
        {
            var response = await _mediator.Send(request);
            return this.GetResponse(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("cache")]
        public async ValueTask<IActionResult> Delete([FromBody] RedisDeleteBasketItemCommand request)
        {
            var response = await _mediator.Send(request);
            return this.GetResponse(response);
        }
    }
}