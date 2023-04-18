using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Configurations;

[ApiController]
// [Authorize]
public class BaseApiController : ControllerBase
{
    private readonly ILogger<BaseApiController> _logger;

    public BaseApiController(ILogger<BaseApiController> logger)
    {
        _logger = logger;
    }

    [Route("error")]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public BaseApiResult Error()
    {
        Response.StatusCode = 404;
        BaseApiResult result = new BaseApiResult { IsSuccess = false };

        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

        if (context is null)
        {
            result.Message = "Not found!";
            return result;
        }

        switch (context.Error.GetType().FullName)
        {
            case "camp-fire.Domain.SeedWork.Exceptions.AuthException":

                Response.StatusCode = 401;
                result.Message = context.Error.Message;
                _logger.LogWarning(result.Message);
                break;
            case "camp-fire.Domain.SeedWork.Exceptions.ApiException":
                Response.StatusCode = 400;
                result.Message = context.Error.Message;
                _logger.LogWarning(result.Message);
                break;
            case "camp-fire.API.Configurations.ValidationException":
                Response.StatusCode = 400;
                //TODO: Bakılacak
                // result.Message = string.Join("; ", ((ValidationException)context.Error).Failures);
                _logger.LogWarning("Error");
                break;
            case "Stripe.StripeException":
                Response.StatusCode = 400;
                result.Message = context.Error.Message;
                _logger.LogCritical(result.Message);
                break;
            default:
                Response.StatusCode = 500;
                result.Message = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development" ? context.Error.Message : "Sorry, something went wrong!";
                _logger.LogError((Exception)context.Error, result.Message + " " + context.Error.InnerException);
                break;
        }

        return result;
    }
}