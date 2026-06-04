using Microsoft.AspNetCore.Mvc;

namespace Invoice.Framework.Common
{
    public class ApiBehaviorExtension
    {
        public static BadRequestObjectResult HandleValidationError(ActionContext context)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var response = OkApiResult<object>.Fail(
                null,
                StatusCodes.Status400BadRequest,
                "اطلاعات ارسالی معتبر نیست"
            );

            response.Data = errors;

            return new BadRequestObjectResult(response);
        }
    }
}
