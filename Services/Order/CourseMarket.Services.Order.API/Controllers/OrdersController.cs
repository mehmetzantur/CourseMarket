using CourseMarket.Services.Order.Application.Commands;
using CourseMarket.Services.Order.Application.Queries;
using CourseMarket.Shared.ControllerBases;
using CourseMarket.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseMarket.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            createOrderCommand.BuyerId = _sharedIdentityService.GetUserId;
            var response = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(response);
        }
    }
}
