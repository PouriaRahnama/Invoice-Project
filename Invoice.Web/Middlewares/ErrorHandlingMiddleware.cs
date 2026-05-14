namespace Invoice.Web.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //var userId = context.GetUserInformation().Item1;
            //var userIP = context.GetUserInformation().Item2;
            //var commandName = context.Request.Path; 
            //context.Response.StatusCode = 500;
            string data = ex.Message.IsPersian() ? ex.Message :
            $"خطای غیرمنتظره‌ای رخ داده است. لطفا با پشتیبانی تماس بگیرید.";

            await context.Response.WriteAsJsonAsync(new OkApiResult<string>()
            {
                Success = false,
                Code = 500,
                Data = data,
                Message = ex.Message
            });
        }
    }
}