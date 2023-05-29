using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Contracts.Orders.Commands;
using CarWashing.Application.Contracts.Orders.Queries;
using CarWashing.Application.Dto.Orders;
using CarWashing.Presentation.Contracts.Orders;

namespace CarWashing.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase {
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator) {
        _mediator = mediator;
    }

    /// <summary>
    /// Registers new order in the system
    /// </summary>
    /// <param name="request">Order information</param>
    /// <returns>Information about created order</returns>
    [HttpPost]
    [Authorize(Roles = CarWashingIdentityRoleNames.AdminRoleName)]
    public async Task<ActionResult<OrderDto>> CreateOrderAsync([FromBody] CreateOrderRequest request) {
        CreateOrder.Command command = new(
            request.Name,
            request.Company,
            request.OrderImage ?? string.Empty,
            request.Price);
        CreateOrder.Response response = await _mediator.Send(command);

        return Ok(response.Order);
    }

    /// <summary>
    /// Gets specified order
    /// </summary>
    /// <param name="orderTime">Order's time</param>
    /// <returns>Information about specified order</returns>
    [HttpGet("at")]
    [Authorize]
    public async Task<ActionResult<IList<OrderDto>>> GetOrderAsync([FromQuery] DateTime orderTime) {
        GetOrder.Query query = new(orderTime);
        GetOrder.Response response = await _mediator.Send(query);

        return Ok(response.Orders);
    }

    /// <summary>
    /// Get sales chart for a date range
    /// </summary>
    /// <param name="From">Start of range</param>
    /// <param name="To">End of range</param>
    /// <returns>Sales chart as PDF document</returns>
    [HttpGet("stats")]
    [Authorize(Roles = CarWashingIdentityRoleNames.AdminRoleName)]
    [Produces("application/pdf", new string[] { })]
    public async Task<ActionResult> GetStats([FromQuery] DateTime From, [FromQuery] DateTime To) {
        GetStats.Query query = new(From, To);
        GetStats.Response response = await _mediator.Send(query);

        return new FileStreamResult(response.Stream, "application/pdf");
    }

    /// <summary>
    /// Get invoice of the order for current user
    /// </summary>
    /// <param name="orderTime">The time of order</param>
    /// <returns>Invoice as PDF document</returns>
    [HttpGet("invoice")]
    [Authorize]
    [Produces("application/pdf", new string[] { })]
    public async Task<ActionResult> GetInvoice([FromQuery] DateTime orderTime) {
        GetInvoice.Query query = new(orderTime);
        GetInvoice.Response response = await _mediator.Send(query);

        return new FileStreamResult(response.Stream, "application/pdf");
    }

    /// <summary>
    /// Get cheque of the order for current user
    /// </summary>
    /// <param name="orderTime">The time of order</param>
    /// <returns>Cheque as PDF document</returns>
    [HttpGet("cheque")]
    [Authorize]
    [Produces("application/pdf", new string[] { })]
    public async Task<ActionResult> GetCheque([FromQuery] DateTime orderTime) {
        GetCheque.Query query = new(orderTime);
        GetCheque.Response response = await _mediator.Send(query);

        return new FileStreamResult(response.Stream, "application/pdf");
    }

    /// <summary>
    /// Lists all orders registered in the system
    /// </summary>
    /// <returns>Information about all orders</returns>
    [HttpGet]
    public async Task<ActionResult<GetAllOrdersResponse>> GetAllOrdersAsync(int? page) {
        GetAllOrders.Query query = new(page ?? 1);
        GetAllOrders.Response response = await _mediator.Send(query);

        return Ok(new GetAllOrdersResponse(
            response.Orders,
            response.Page,
            response.TotalPages));
    }
}
