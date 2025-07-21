using Microsoft.AspNetCore.Mvc;
using Worker.API.Controllers.Responses;
using Worker.API.Helpers;

namespace Worker.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SchedulerController(ILogger<SchedulerController> logger) : ControllerBase
{
    [HttpGet]
    public IEnumerable<JobResponse> Get()
    {
        return [new JobResponse() { Date = Instant.DateNow, Name = "Test Job" }];
    }
}
