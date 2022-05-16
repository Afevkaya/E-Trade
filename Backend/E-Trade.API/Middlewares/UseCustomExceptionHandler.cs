using E_Trade.Core.DTOs;
using E_Trade.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace E_Trade.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        // Custom Exception middleware
        // IApplicationBuilder -> Middleware yazabilmek için kullanılan interface
        public static void UseCustomException(this IApplicationBuilder app)
        {
            // UseExceptionHandler -> .Net Core Framework'ünün Hataları yakalamak için hazır sunduğu middleware  
            app.UseExceptionHandler(config =>
            {
                // Kesici middleware
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
