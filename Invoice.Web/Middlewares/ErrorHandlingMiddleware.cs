using Microsoft.AspNetCore.Http;

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
            int statusCode = ex switch
            {
                //NotFoundException => StatusCodes.Status404NotFound,
                //ConflictException => StatusCodes.Status409Conflict,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                BusinessException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            /*
                200 → موفق
                400 → خطای کاربر (Client Error)
                500 → خطای سرور (Server Error)
            
             */

            // تشخیص اینکه خطا از نوع بیزنس است یا سیستمی
            if (ex is BusinessException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }

            context.Response.StatusCode = statusCode;
            string data = ex.Message.IsPersian() ? ex.Message :
            $"خطای غیرمنتظره‌ای رخ داده است. لطفا با پشتیبانی تماس بگیرید.";

            await context.Response.WriteAsJsonAsync(OkApiResult<string>.Fail(
                 null, statusCode, ex.Message));
        }
    }
}