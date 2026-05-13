using Microsoft.AspNetCore.Authorization;

namespace Invoice.Web.Controllers;


public class TestController : ApiBaseController
{
    public TestController(ILogger<ApiBaseController> logger) : base(logger)
    {
    }

    [HttpGet(Name = "Auth")]
    [AllowAnonymous]
    public IActionResult Get()
    {
        var x = User;
        return Ok("done !!!");
    }
}

