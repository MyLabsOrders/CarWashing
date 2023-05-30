using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Contracts.Users.Commands;
using CarWashing.Application.Contracts.Users.Queries;
using CarWashing.Application.Dto.Users;
using CarWashing.Presentation.Contracts.Users;

namespace CarWashing.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates user account with all his personnel data
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityId">identity id that is trying to create profile</param>
    /// <returns>Information about created account</returns>
    [HttpPost("{identityId:guid}/profile")]
    [Authorize]
    public async Task<ActionResult<UserDto>> CreateUserAsync(Guid identityId, [FromBody] CreateUserRequest request) {
        CreateUser.Command command = new(
            identityId,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.UserImage ?? string.Empty,
            request.BirthDate,
            request.PhoneNumber,
            request.Gender);

        CreateUser.Response response = await _mediator.Send(command);

        return Ok(response);
    }

    /// <summary>
    /// Updates user account's properties
    /// </summary>
    /// <param name="identityId">identity id that is trying to update profile</param>
    /// <param name="request"></param>
    /// <returns>Information about created account</returns>
    [HttpPatch("{identityId:guid}/profile")]
    [Authorize]
    public async Task<ActionResult<UserDto>> UpdateUserAsync(Guid identityId, [FromBody] UpdateUserRequest request) {
        UpdateUser.Command command = new(
            identityId,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.UserImage,
            request.BirthDate,
            request.PhoneNumber,
            request.Gender,
            request.IsActive);

        UpdateUser.Response response = await _mediator.Send(command);

        return Ok(response);
    }

    /// <summary>
    /// Adds order to user purchase bucket
    /// </summary>
    /// <param name="userId">Target user id</param>
    /// <param name="request">Target orders id, rent days and amount</param>
    /// <returns>Order's timestamp</returns>
    [HttpPut("{userId:guid}/orders")]
    [Authorize(Roles = CarWashingIdentityRoleNames.DefaultUserRoleName)]
    public async Task<ActionResult<DateTime>> AddOrderAsync(Guid userId, [FromBody] IList<AddOrderRequest> request) {
        AddOrder.Command command = new(
            userId,
            request.Select(order => (order.OrderId, order.Count)).ToList());
        AddOrder.Response response = await _mediator.Send(command);

        return Ok(response.OrderTime);
    }

    /// <summary>
    /// Removes order from user purchase bucket
    /// </summary>
    /// <param name="userId">Target user id</param>
    /// <param name="request">Target order id</param>
    /// <returns></returns>
    [HttpDelete("{userId:guid}/orders")]
    [Authorize(Roles = CarWashingIdentityRoleNames.DefaultUserRoleName)]
    public async Task<IActionResult> RemoveOrderAsync(Guid userId, [FromBody] RemoveOrderRequest request) {
        RemoveOrder.Command command = new(request.OrderId, userId);
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Gets specified user
    /// </summary>
    /// <param name="id">Target user id</param>
    /// <returns>Information about specified user</returns>
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetUserAsync(Guid id) {
        GetUser.Query query = new(id);
        GetUser.Response response = await _mediator.Send(query);

        return Ok(response.User);
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <returns>All users</returns>
    [HttpGet]
    [Authorize(Roles = CarWashingIdentityRoleNames.AdminRoleName)]
    public async Task<ActionResult<GetAllUsersResponse>> GetUsersAsync(int? page) {
        GetAllUsers.Query query = new(page ?? 1);
        GetAllUsers.Response response = await _mediator.Send(query);

        return Ok(new GetAllUsersResponse(
            response.Users,
            response.Page,
            response.TotalPages));
    }

    /// <summary>
    /// Make deposit to customer account
    /// </summary>
    /// <param name="id">Target customer id</param>
    /// <param name="request">Money amount to be replenished</param>
    /// <returns></returns>
    [HttpPut("{id:guid}/account")]
    [Authorize]
    public async Task<IActionResult> MakeDepositAsync(Guid id, [FromBody] ReplenishBalanceRequest request) {
        ReplenishBalance.Command command = new(id, request.Total);
        await _mediator.Send(command);

        return Ok();
    }

}
