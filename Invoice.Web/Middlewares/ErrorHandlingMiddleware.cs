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
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BusinessException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            string message = statusCode switch
            {
                StatusCodes.Status400BadRequest =>
                    ex.Message.IsPersian() ? ex.Message : "درخواست ارسال شده نامعتبر است.",

                StatusCodes.Status401Unauthorized =>
                    "شما مجوز دسترسی به این بخش را ندارید. لطفاً وارد حساب کاربری خود شوید.",

                StatusCodes.Status404NotFound =>
                    ex.Message.IsPersian() ? ex.Message : "منبع یا اطلاعات مورد نظر یافت نشد.",

                StatusCodes.Status500InternalServerError =>
                    "خطای غیرمنتظره‌ای در سرور رخ داده است. لطفاً بعداً دوباره تلاش کنید.",

                _ =>
                    "خطایی در پردازش درخواست رخ داده است."
            };

            /*
               200 → موفق
               400 → خطای کاربر / درخواست نامعتبر
               401 → عدم احراز هویت / عدم دسترسی
               404 → منبع پیدا نشد
               500 → خطای داخلی سرور
            */
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(OkApiResult<string>.Fail(
                 null, statusCode, message));
        }
    }
}