namespace Invoice.Web.Controllers;

public class AuthController : BaseController
{
    public AuthController(ILogger<BaseController> logger) : base(logger)
    {
    }

    [HttpGet(Name = "Auth")]
    public IActionResult Get()
    {
        return Ok("done !!!");
    }
}

