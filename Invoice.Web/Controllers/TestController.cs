using Invoice.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

