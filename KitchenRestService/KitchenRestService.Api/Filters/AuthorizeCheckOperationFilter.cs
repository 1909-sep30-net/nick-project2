using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KitchenRestService.Api.Filters
{
    // allows Swagger to understand that [Authorize] attribute means
    // that Bearer auth is required, or else 401 response.
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true);
            var methodAttributes = context.MethodInfo.GetCustomAttributes(true);

            var authorize = !methodAttributes.OfType<AllowAnonymousAttribute>().Any() &&
                methodAttributes.OfType<AuthorizeAttribute>().Any() ||
                controllerAttributes.OfType<AuthorizeAttribute>().Any();

            if (!authorize)
                return;

            operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse { Description = "Unauthorized" });
            
            var bearerScheme = new OpenApiSecurityScheme
            {
                // the id here must match the security scheme name defined in the startup file
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerAuth" }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                // scope list should be empty when definition type is bearer
                new OpenApiSecurityRequirement { [ bearerScheme ] = Array.Empty<string>() }
            };
        }
    }
}
