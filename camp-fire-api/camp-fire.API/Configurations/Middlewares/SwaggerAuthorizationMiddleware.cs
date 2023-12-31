namespace camp_fire.API.Configurations.Middlewares;

// public class SwaggerAuthorizationMiddleware
// {
//     private readonly RequestDelegate _next;
//     private readonly IConfiguration _configuration;

//     public SwaggerAuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
//     {
//         _next = next;
//         _configuration = configuration;
//     }

//     public async Task Invoke(HttpContext context)
//     {
//         if (!context.Request.Path.Value!.Contains("/swagger"))
//             await _next.Invoke(context);

//         if (context.Request.Path.Value.Contains(".css") || context.Request.Path.Value.EndsWith(".js") || context.Request.Path.Value.Contains(".png"))
//             await _next.Invoke(context);

//         if (context.Request.Query["swaggerKey"] != _configuration["Authentication:SwaggerKey"])
//         {
//             return;
//         }

//         await _next.Invoke(context);
//     }
// }
