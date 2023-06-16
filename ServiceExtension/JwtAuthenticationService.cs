using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace SecondhandStore.ServiceExtension;

public static class JwtAuthenticationService
{
    public static void AddJwtAuthenticationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Remember to set to true on production
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                        (configuration["JwtToken:NotTokenKeyForSureSourceTrustMeDude"])),
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtToken:Issuer"],
                    ValidAudience = configuration["JwtToken:Audience"]
                };
                // Trigger event liên quan tới JWT như là chưa có quyền truy cập, token hết hạn, ...
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        // Call this to skip the default logic and avoid using the default response
                        context.HandleResponse();

                        context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");

                        context.Response.ContentType = "application/json";

                        var httpContext = context.HttpContext;

                        const int statusCode = StatusCodes.Status401Unauthorized;

                        var routeData = httpContext.GetRouteData();
                        var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());

                        var factory = httpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                        var problemDetails = factory.CreateProblemDetails(httpContext, statusCode);

                        var result = new ObjectResult(problemDetails)
                        {
                            StatusCode = statusCode,
                            DeclaredType = typeof(ProblemDetails)
                        };

                        await result.ExecuteResultAsync(actionContext);
                    }
                };
            });
    }
}