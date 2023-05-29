using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Contracts.Identity.Commands;
using CarWashing.Application.Contracts.Identity.Queries;
using CarWashing.Presentation.Contracts.Identity;

namespace CarWashing.Presentation.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase {
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator) {
        _mediator = mediator;
    }

    /// <summary>
    /// Logs in user in the system.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Jwt access token</returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LoginRequest request) {
        Login.Query query = new(request.Username, request.Password);
        Login.Response response = await _mediator.Send(query, HttpContext.RequestAborted);

        LoginResponse loginResponse = new(response.UserId, response.Token);
        return Ok(loginResponse);
    }

    /// <summary>
    /// Changes user role to the given one
    /// </summary>
    /// <param name="username">Target username</param>
    /// <param name="roleName">User new role</param>
    /// <returns></returns>
    [HttpPut("users/{username}/role")]
    [Authorize(Roles = CarWashingIdentityRoleNames.AdminRoleName)]
    public async Task<IActionResult> ChangeUserRoleAsync(string username, [FromQuery] string roleName) {
        ChangeUserRole.Command command = new(username, roleName);
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Registers user in a system
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Jwt access token</returns>
    [HttpPost("user/register")]
    public async Task<ActionResult<CreateUserAccountResponse>> CreateUserAccountAsync([FromBody] CreateUserAccountRequest request) {
        CreateUserAccount.Command command = new(request.Username, request.Password, request.RoleName);
        CreateUserAccount.Response response = await _mediator.Send(command);

        CreateUserAccountResponse registerResponse = new(response.Token);
        return Ok(registerResponse);
    }

    /// <summary>
    /// Changes current user name to the provided one
    /// </summary>
    /// <param name="request">New username</param>
    /// <returns>Jwt access token</returns>
    [HttpPut("username")]
    [Authorize]
    public async Task<ActionResult<UpdateUsernameResponse>> UpdateUsernameAsync([FromBody] UpdateUsernameRequest request) {
        UpdateUsername.Command command = new(request.Username);
        UpdateUsername.Response response = await _mediator.Send(command);

        return Ok(new UpdateUsernameResponse(response.Token));
    }

    /// <summary>
    /// Changes current user password to the provided one
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Jwt access token</returns>
    [HttpPut("password")]
    [Authorize]
    public async Task<ActionResult<UpdatePasswordResponse>> UpdatePasswordAsync([FromBody] UpdatePasswordRequest request) {
        UpdatePassword.Command command = new(request.CurrentPassword, request.NewPassword);
        UpdatePassword.Response response = await _mediator.Send(command);

        return Ok(new UpdatePasswordResponse(response.Token));
    }

    /// <summary>
    /// Checks whether user is admin
    /// </summary>
    /// <param name="request">user name</param>
    /// <returns>200 if user is admin</returns>
    [HttpPost("authorize-admin")]
    [Authorize]
    public async Task<IActionResult> AuthorizeAdminAsync([FromBody] AuthorizeAdminRequest request) {
        AuthorizeAdmin.Query query = new AuthorizeAdmin.Query(request.Username);
        await _mediator.Send(query);

        return Ok();
    }

    /// <summary>
    /// Provides information about identity.Pass identity or user id.
    /// </summary>
    /// <param name="userId">identity or user id</param>
    /// <returns>Username and user role</returns>
    [HttpGet("{userId:guid}")]
    [Authorize(Roles = CarWashingIdentityRoleNames.AdminRoleName)]
    public async Task<ActionResult<GetIdentityResponse>> GetRoleByIdAsync(Guid userId) {
        GetIdentity.Query query = new(userId);
        GetIdentity.Response response = await _mediator.Send(query);

        return Ok(new GetIdentityResponse(response.Username, response.Role));
    }
}