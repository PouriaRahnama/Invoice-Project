namespace Invoice.Web.Controllers;

public class AuthController : ApiBaseController
{
    public AuthController(ILogger<ApiBaseController> logger) : base(logger)
    {
    }

    [HttpGet(Name = "Auth")]
    public IActionResult Get()
    {
        return Ok("done !!!");
    }
}

