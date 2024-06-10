using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SettingsService.Handlers;

namespace SettingsService.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ILogger<SettingsController> _logger;
    private readonly IMediator _mediator;

    public SettingsController(ILogger<SettingsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(Name = "PostSetting")]
    public async Task<ActionResult> Post(string setting)
    {
        _logger.LogInformation("Calling post setting");
        var xTenant = Request.Headers.SingleOrDefault(x => x.Key == "X-Tenant").Value.FirstOrDefault();
        if (xTenant == null)
        {
            return BadRequest("No tenant");
        }
        
        var result = await _mediator.Send(new CreateSettingCommand { Setting = setting, XTenant = xTenant });

        if (result.IsSuccess)
        {
            return new OkResult();
        }
        return BadRequest(result.Errors);
    }
}
