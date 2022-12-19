// using System.Net;
// using HilfeOhneGrenzen.Domain.SeedWork.Helpers;
// using Microsoft.AspNetCore.Diagnostics;

// namespace HilfeOhneGrenzen.API.Configurations;

// public static class ExceptionMiddlewareProvider
// {
//     public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
//     {
//         app.UseExceptionHandler(appError =>
//         {
//             appError.Run(async context =>
//             {
//                 BaseApiResult result = new BaseApiResult { IsSuccess = false };

//                 if (context.Response.StatusCode is (int)HttpStatusCode.NotFound)
//                 {
//                     context.Response.StatusCode = (int)HttpStatusCode.NotFound;
//                     result.Message = "Not found!";
//                     await context.Response.WriteAsync(result.ToJson());
//                 }

//                 var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

//                 if (contextFeature is null)
//                 {
//                     result.Message = "Not found!";

//                     await context.Response.WriteAsync(result.ToJson());
//                 }

//                 switch (contextFeature?.Error.GetType().FullName)
//                 {
//                     case "HilfeOhneGrenzen.Domain.SeedWork.Exceptions.AuthException":

//                         context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//                         result.Message = contextFeature.Error.Message;
//                         logger.LogWarning(result.Message);
//                         break;
//                     case "HilfeOhneGrenzen.Domain.SeedWork.Exceptions.ApiException":
//                         context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
//                         result.Message = contextFeature.Error.Message;
//                         logger.LogWarning(result.Message);
//                         break;
//                     case "HilfeOhneGrenzen.API.Configurations.ValidationException":
//                         context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
//                         result.Message = ((ValidationException)contextFeature.Error).Failures.ToJson();
//                         logger.LogWarning(result.Message);
//                         break;
//                     default:
//                         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//                         result.Message = "Sorry, something went wrong!";
//                         logger.LogError(result.Message);
//                         break;
//                 }

//                     //TODO: Loglama mekanizması oluştur


//                     await context.Response.WriteAsync(result.ToJson());
//             });
//         });
//     }
// }
