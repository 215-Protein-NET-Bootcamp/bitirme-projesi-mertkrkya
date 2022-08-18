using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UrunKatalogProjesi.Core.Entities;
using UrunKatalogProjesi.Service.Exceptions;

namespace UrunKatalogProjesi.API.Middleware
{
    public static class ExceptionHandlerMiddleware
    {
        public static void CustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientException => 400,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    var response = new ResponseEntity(errorMessage: exceptionFeature.Error.Message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
