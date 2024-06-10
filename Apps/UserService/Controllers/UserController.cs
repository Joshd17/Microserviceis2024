using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Handlers;

namespace UserService.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(Name = "PostUser")]
    public async Task<ActionResult> Post(string userName)
    {
        _logger.LogInformation("Calling post user");
        var xTenant = Request.Headers.SingleOrDefault(x => x.Key == "X-Tenant").Value.FirstOrDefault();
        if (xTenant == null)
        {
            return BadRequest("No tenant");
        }
        var result = await _mediator.Send(new CreateUserCommand { UserName = userName, XTenant = xTenant });
        
        if (result.IsSuccess)
        {
            return new OkResult();
        }
        return BadRequest(result.Errors);
    }
}
