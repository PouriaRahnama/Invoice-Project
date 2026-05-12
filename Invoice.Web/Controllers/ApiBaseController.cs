using Invoice.Application.Interfaces;

namespace Invoice.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ApiBaseController : ControllerBase
{
    private readonly ILogger<ApiBaseController> _logger;

    public ApiBaseController(ILogger<ApiBaseController> logger)
    {
        _logger = logger;
    }

    private IProductService productService;
    protected IProductService _productService => productService 
        ??= HttpContext.RequestServices.GetRequiredService<IProductService>();


    private IUserService userService;
    protected IUserService _userService => userService
        ??= HttpContext.RequestServices.GetRequiredService<IUserService>();




}

