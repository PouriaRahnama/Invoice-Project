namespace Invoice.Web.Controllers;
public class TestController : ApiBaseController
{
    public TestController(ILogger<ApiBaseController> logger) : base(logger)
    {
    }

    [HttpGet(Name = "Test")]
    [AllowAnonymous]
    public IActionResult Get()
    {
        var q = User;
        //var a = 1;
        //var b = 0;
        //var x = a/b;
        return Ok("done !!!");
    }
}

